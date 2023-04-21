using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthNDeath : MonoBehaviour
{
    public int health;

    private bool alive = true;
    public GameObject sceneManager;

    void Update()
    {
        if (alive && health <= 0)
        {
            Die();
        }
    }

    void Die() // Desativa todos as funções e parâmetros importantes do personagem
    {
        alive = false;
        gameObject.GetComponent<CharBaseMov>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<AudioPlayer>().PlayAudio("death");
        gameObject.GetComponent<PlayerParticles>().Explode();
        StartCoroutine(deathReload(2f));
    }

    IEnumerator deathReload(float t) // Recarrega o nível depois de algum tempo t (em segundos)
    {
        yield return new WaitForSeconds(t);
        sceneManager.GetComponent<SceneManage>().ChangeScene("Reload");
    }
}
