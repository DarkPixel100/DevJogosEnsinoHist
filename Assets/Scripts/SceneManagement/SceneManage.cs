using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public void ChangeScene(string sceneName) // Muda cena
    {
        if (sceneName == "Reload") SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarrega o nível
        else if (sceneName == "Menu") SceneManager.LoadScene("Menu"); // Vai para o menu
        else if (sceneName == "NewGame") // Novo jogo
        {
            PlayerPrefs.SetString("CurrentLevel", "Cutscene0"); // Define o nível do save como 0 (primeira cutscene)
            SceneManager.LoadScene("Cutscene0"); // Carrega a primeira cutscene
        }
        else if (sceneName == "LevelChooser") { } //Talvez nunca seja usado
        else if (sceneName == "NextLevel") // Próximo nível
        {
            int nextIndex = SceneManager.GetActiveScene().buildIndex + 1; // Pega o próximo índice
            string path = SceneUtility.GetScenePathByBuildIndex(nextIndex); // Pega o caminho do próximo nível
            string scName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1); // Pega o nome do próximo nível
            if (scName != "Credits") // Verifica se está fora dos créditos
            {
                PlayerPrefs.SetString("CurrentLevel", scName); // Salva o nível atual nas preferências
                PlayerPrefs.Save();
            }
            SceneManager.LoadScene(nextIndex); // Carrega a próxima cena
        }
    }
}
