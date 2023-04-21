using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private LayerMask damagingLayer; // Layer de coisas que danificam o jogador ao entrar em contato
    private GameObject healthMeter;
    private bool canBeHit; // Pode receber dano
    public float hittableDelay;
    public float movementDelay;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthMeter = GameObject.Find("HealthMeter");
        healthMeter.GetComponent<HealthMeterKeeper>().UpdateHealth(GetComponent<HealthNDeath>().health);
        canBeHit = true;
    }

    private void OnTriggerEnter2D(Collider2D damagingElement) // Verifica a colisão
    {
        if (((1 << damagingElement.gameObject.layer) & damagingLayer) != 0 && canBeHit) // Só permite o dano se for de algo no layer correto e a bool canBeHit for true
        {
            if (damagingElement.name == "BottomlessTrigger") // Se cair num buraco/do mapa, morre instantaneamente
            {
                GetComponent<HealthNDeath>().health = 0;
                healthMeter.GetComponent<HealthMeterKeeper>().UpdateHealth(GetComponent<HealthNDeath>().health);
            }
            else if (rb.velocity.y >= -1e-04 || !damagingElement.name.Contains("Enemy")) // Se for acertado por um projétil, leva 1 de dano
            {
                GetComponent<HealthNDeath>().health -= 1; // Dano
                canBeHit = false;
                
                GetComponent<AudioPlayer>().PlayAudio("hit"); // Áudio de dano
                
                StartCoroutine(hitAllowTimer(hittableDelay)); // Delay de invencibilidade
                healthMeter.GetComponent<HealthMeterKeeper>().UpdateHealth(GetComponent<HealthNDeath>().health); // Atualiza a vida

                rb.simulated = false; // Para o movimento do player
                StartCoroutine(stoppedTimer(movementDelay)); // Delay de movimento
            }
            else // Se estiver descendo (com velocidade vertical < 0) e entrar em contato com um inimigo, mata ele
            {
                damagingElement.gameObject.GetComponent<EnemyParticles>().Explode();
                if (!damagingElement.gameObject.GetComponent<EnemyMovement>().alternatingWalk)
                {
                    damagingElement.gameObject.GetComponent<ThrowAttack>().enabled = false;
                    damagingElement.gameObject.GetComponent<Animator>().enabled = false;
                }
                damagingElement.gameObject.GetComponent<EnemyMovement>().enabled = false;
                damagingElement.gameObject.GetComponent<Rigidbody2D>().simulated = false;
                damagingElement.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                damagingElement.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<AudioPlayer>().PlayAudio("death");
            }
        }
    }
    IEnumerator hitAllowTimer(float t) // Espera um tempo t (em segundos) para poder receber dano novamente
    {
        yield return new WaitForSeconds(t);
        canBeHit = true;
    }

    IEnumerator stoppedTimer(float t) // Espera um tempo t (em segundos) para poder se mover novamente
    {
        yield return new WaitForSeconds(t);
        rb.simulated = true;
    }
}
