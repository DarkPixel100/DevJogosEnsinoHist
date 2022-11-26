using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    [SerializeField] private LayerMask collectableLayer;
    
    public GameObject sceneManager;

    private void OnTriggerEnter2D(Collider2D collectable)
    {
        if (((1 << collectable.gameObject.layer) & collectableLayer) != 0)
        {
            switch (collectable.gameObject.name)
            {
                case "Heart":
                    if (gameObject.GetComponent<HealthNDeath>().health < 3) gameObject.GetComponent<HealthNDeath>().health++;
                    break;
                case "Objective":
                    StartCoroutine(pickupDelay(2f));
                    break;
            }
            collectable.gameObject.SetActive(false);
        }
    }

    IEnumerator pickupDelay (float t)
    {
        yield return new WaitForSeconds(t);
        sceneManager.GetComponent<SceneManage>().ChangeScene("NextLevel");
    }
}
