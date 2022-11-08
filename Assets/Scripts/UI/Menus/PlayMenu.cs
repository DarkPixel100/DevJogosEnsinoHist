using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMenu : MonoBehaviour
{
    public GameObject stateManager;
    public void NewGame()
    {
        stateManager.GetComponent<SceneManage>().ChangeScene("NewGame");
        PlayerPrefs.SetString("CurrentLevel", "Level1");
    }

    public void Continue()
    {
        // Debug.Log(PlayerPrefs.GetString("CurrentLevel"));
        stateManager.GetComponent<SceneManage>().ChangeScene(PlayerPrefs.GetString("CurrentLevel"));
    }
}
