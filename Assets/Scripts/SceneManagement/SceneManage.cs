using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        if (sceneName == "Reload") SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else if (sceneName == "Menu") SceneManager.LoadScene("Menu");
        else if (sceneName == "NewGame")
        {
            PlayerPrefs.SetString("CurrentLevel", "Cutscene1-0");
            SceneManager.LoadScene("Cutscene1-0");
        }
        else if (sceneName == "LevelChooser"){} //Talvez nunca seja usado
        else if (sceneName == "NextLevel")
        {
            int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
            string path = SceneUtility.GetScenePathByBuildIndex(nextIndex);
            string scName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);
            PlayerPrefs.SetString("CurrentLevel", scName);
            PlayerPrefs.Save();
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            PlayerPrefs.SetString("CurrentLevel", sceneName);
            SceneManager.LoadScene(sceneName);
        }
    }
}
