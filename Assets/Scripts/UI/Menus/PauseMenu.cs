using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject sceneManager;
    public bool gameIsPaused;

    public GameObject pauseMenuDarken;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameIsPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        };
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        pauseMenuDarken.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        pauseMenuDarken.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Options()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    public void ExitOptions()
    {
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        sceneManager.GetComponent<SceneManage>().ChangeScene("Menu");
    }
}
