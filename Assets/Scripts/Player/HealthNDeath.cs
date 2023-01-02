using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthNDeath : MonoBehaviour
{
    public int health;
    public GameObject sceneManager;

    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.GetComponent<CharBaseMov>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(deathReload(2f));
    }

    IEnumerator deathReload(float t)
    {
        yield return new WaitForSeconds(t);
        sceneManager.GetComponent<SceneManage>().ChangeScene("Reload");
    }
}
