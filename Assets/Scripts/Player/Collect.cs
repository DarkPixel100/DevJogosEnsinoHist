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

    private void OnTriggerEnter2D(Collider2D collectable)
    {
        if (((1 << collectable.gameObject.layer) & collectableLayer) != 0)
        {
            switch (collectable.gameObject.name)
            {
                case "HealthHeart":
                    if (GetComponent<HealthNDeath>().health < 3)
                    {
                        GetComponent<HealthNDeath>().health++;
                        HealthMeter.GetComponent<HealthMeterKeeper>().UpdateHealth(GetComponent<HealthNDeath>().health);
                        GetComponent<AudioPlayer>().PlayAudio("collectHealth");
                    }
                    break;
                case "Objective":
                        GetComponent<AudioPlayer>().PlayAudio("collectObjective");
                    StartCoroutine(pickupDelay(2f));
                    break;
            }
            collectable.gameObject.SetActive(false);
        }
    }

    IEnumerator pickupDelay(float t)
    {
        yield return new WaitForSeconds(t);
        sceneManager.GetComponent<SceneManage>().ChangeScene("NextLevel");
    }
}
