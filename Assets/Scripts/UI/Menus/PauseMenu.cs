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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Abre ao apertar Esc
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
        pauseMenuUI.SetActive(true); // Ativa o menu
        pauseMenuDarken.SetActive(true); // Ativa o overlay para escurecer o fundo
        Time.timeScale = 0f; // Para o tempo do jogo
        gameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Desativa o menu
        pauseMenuDarken.SetActive(false); // Desativa o overlay para escurecer o fundo
        Time.timeScale = 1f; // Volta o tempo do jogo ao normal
        gameIsPaused = false;
    }

    public void Options() // Abre o menu "Opções"
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    public void ExitOptions() // Volta do menu "Opções"
    {
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
    }

    public void ReturnToMenu() // Volta para o menu principal
    {
        Time.timeScale = 1f;
        sceneManager.GetComponent<SceneManage>().ChangeScene("Menu");
    }
}
