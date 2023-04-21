using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    [SerializeField] private LayerMask collectableLayer;

    public GameObject sceneManager;

    private GameObject HealthMeter;

    void Start()
    {
        HealthMeter = GameObject.Find("HealthMeter");
    }

    private void OnTriggerEnter2D(Collider2D collectable) // Verifica colisão
    {
        if (((1 << collectable.gameObject.layer) & collectableLayer) != 0) // Se o objeto com o qual colidiu foi um coletável, interaja
        {
            switch (collectable.gameObject.name)
            {
                case "HealthHeart": // Se for um coração, aumenta a vida
                    if (GetComponent<HealthNDeath>().health < 3)
                    {
                        GetComponent<HealthNDeath>().health++; // Aumenta vida
                        HealthMeter.GetComponent<HealthMeterKeeper>().UpdateHealth(GetComponent<HealthNDeath>().health); // Atualiza vida
                        GetComponent<AudioPlayer>().PlayAudio("collectHealth"); // Áudio de coletar
                    }
                    break;
                case "Objective": // Se for um objetivo, finaliza o nível
                        GetComponent<AudioPlayer>().PlayAudio("collectObjective"); // Áudio de coletar
                    StartCoroutine(pickupDelay(2f)); // Espera até terminar o nível
                    break;
            }
            collectable.gameObject.SetActive(false); // Remove o objeto
        }
    }

    IEnumerator pickupDelay(float t) // Espera um tempo t (em segundos) para passar para o próximo nível
    {
        yield return new WaitForSeconds(t);
        sceneManager.GetComponent<SceneManage>().ChangeScene("NextLevel");
    }
}
