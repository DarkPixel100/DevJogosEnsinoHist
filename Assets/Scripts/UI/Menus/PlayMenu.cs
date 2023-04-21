using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMenu : MonoBehaviour
{
    public GameObject sceneManager;
    public void NewGame() // Botão "Novo Jogo"
    {
        sceneManager.GetComponent<SceneManage>().ChangeScene("NewGame");
    }

    public void Continue() // Botão "Continuar"
    {
        sceneManager.GetComponent<SceneManage>().ChangeScene(PlayerPrefs.GetString("CurrentLevel"));
    }
}
