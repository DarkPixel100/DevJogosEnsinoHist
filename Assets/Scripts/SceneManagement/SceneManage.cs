using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        switch (sceneName)
        {
            case "Reload":
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case "Menu":
                SceneManager.LoadScene("Menu");
                break;
            case "NewGame":
                SceneManager.LoadScene("Cutscene1");
                break;
            case "LevelChooser": //Talvez nunca seja usado
                break;
            case "Level1":
                PlayerPrefs.SetString("CurrentLevel", "Level1");
                SceneManager.LoadScene("Level1");
                break;
            case "Level2":
                PlayerPrefs.SetString("CurrentLevel", "Level3");
                SceneManager.LoadScene("Level2");
                break;
            case "Level3":
                PlayerPrefs.SetString("CurrentLevel", "Level3");
                SceneManager.LoadScene("Level3");
                break;
            case "Level4":
                PlayerPrefs.SetString("CurrentLevel", "Level4");
                SceneManager.LoadScene("Level4");
                break;
            case "Level5":
                PlayerPrefs.SetString("CurrentLevel", "Level5");
                SceneManager.LoadScene("Level5");
                break;
            case "NextLevel":
                int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
                string path = SceneUtility.GetScenePathByBuildIndex(nextIndex);
                string scName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);
                PlayerPrefs.SetString("CurrentLevel", scName);
                SceneManager.LoadScene(nextIndex);
                break;
        }
    }
}
