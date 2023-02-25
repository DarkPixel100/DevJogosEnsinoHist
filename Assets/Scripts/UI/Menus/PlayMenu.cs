using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMenu : MonoBehaviour
{
    public GameObject sceneManager;
    public void NewGame()
    {
        sceneManager.GetComponent<SceneManage>().ChangeScene("NewGame");
        PlayerPrefs.SetString("CurrentLevel", "Level1");
    }

    public void Continue()
    {
        sceneManager.GetComponent<SceneManage>().ChangeScene(PlayerPrefs.GetString("CurrentLevel"));
    }
}
