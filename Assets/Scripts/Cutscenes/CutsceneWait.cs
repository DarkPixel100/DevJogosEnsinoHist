using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneWait : MonoBehaviour
{
    public float waitTime;
    public GameObject stateManager;
    void Start()
    {
        StartCoroutine(finishCutscene(waitTime));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button1)) stateManager.GetComponent<SceneManage>().ChangeScene("NextLevel");
    }

    IEnumerator finishCutscene(float t)
    {
        yield return new WaitForSeconds(t);
        stateManager.GetComponent<SceneManage>().ChangeScene("NextLevel");
    }
}
