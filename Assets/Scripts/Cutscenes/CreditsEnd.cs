using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsEnd : MonoBehaviour
{
    public GameObject sceneManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button1)) EndCredits();
    }

    public void EndCredits()
    {
        sceneManager.GetComponent<SceneManage>().ChangeScene("Menu");
    }
}
