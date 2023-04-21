using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAttack : MonoBehaviour
{
    public float throwInterval; // Tempo entre cada ataque
    public float throwVel; // Velocidade inicial do projétil
    public GameObject projectilePref; // Objeto a ser jogado
    private int lookDir;

    private GameObject player;
    void Start()
    {
        player = GameObject.Find("Player"); ;
        if (GetComponent<EnemyMovement>().alternatingWalk)
        {
            this.enabled = false; // Desativa esse script caso o inimigo não ataque à distância
        }
        else
        {
            StartCoroutine(throwRepeat(throwInterval)); // Inicia a sequência de ataques

        }
    }
    void Update()
    {
        lookDir = GetComponent<EnemyMovement>().lookDir;
    }

    IEnumerator throwRepeat(float t)
    {
        while (true)
        {
            yield return new WaitForSeconds(t);
            if (Mathf.Abs(transform.position.x - player.transform.position.x) <= 12) // Player só será atacado se estiver a menos de 12 unidades horizontalmente
            {
                GetComponent<Animator>().SetTrigger("Throw"); // Inicia a animação de jogar (ela tem um animation event que inicia a func shoot)
            }
        }
    }

    private void shoot()
    {
        // Posição inicial do projétil
        Vector3 instPos = new Vector3(transform.position.x + transform.localScale.x * lookDir, transform.position.y + transform.localScale.y / 2, 0);

        // Posição do alvo
        Vector3 posDiff = player.transform.position - instPos;

        // Instansiando o projétil
        GameObject projReal = Instantiate(projectilePref, new Vector3(instPos.x, instPos.y, transform.position.z), Quaternion.identity);
        GetComponent<AudioSource>().Play(); // Som do ataque
        projReal.GetComponent<ProjectileFunctions>().rotationDirection = -lookDir; // Direção do ataque
        projReal.GetComponent<SpriteRenderer>().flipX = GetComponent<SpriteRenderer>().flipX; // Inversão do sprite baseada na direçào do ataque
        projReal.GetComponent<Rigidbody2D>().velocity = Vector3.Normalize(posDiff) * throwVel; // Velocidade inicial do ataque baseada na posição (normalizada) do alvo
    }
}
