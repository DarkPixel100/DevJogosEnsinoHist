using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptSetter : MonoBehaviour
{
    private TextAsset scriptAsset;

    private string path;
    private string scName;

    public string[] linesArray;

    [HideInInspector]
    public GameObject[] textBoxes;
    void Start()
    {
        textBoxes = GameObject.FindGameObjectsWithTag("Dialogue");
        string path = SceneUtility.GetScenePathByBuildIndex(SceneManager.GetActiveScene().buildIndex);
        string scName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);
        scriptAsset = Resources.Load<TextAsset>("Dialogue" + scName[scName.Length - 1]);

        string script = scriptAsset.text;

        script = script.Replace("\r\n1:", "<switch><page>").Replace("\r\n2:", "<switch><page>").Replace("\r\n", "<page>").Replace("<switch><page>", "<switch><page>\r\n").Replace("1:", "<line-indent=1em>");
        script = script.Substring(0, script.IndexOf("\r\n") + 2) + "<line-indent=1em>" + script.Substring(script.IndexOf("\r\n") + 2);
        linesArray = script.Split("\r\n");

        for (int i = 0; i < linesArray.Length; i++)
        {
            if(linesArray.Length >= 2 && i == linesArray.Length - 2)
            {
                linesArray[i] = linesArray[i].Substring(0, linesArray[i].LastIndexOf("<page>"));
            }
            textBoxes[i % 2].GetComponent<TMPro.TMP_Text>().text += linesArray[i];
        }
        textBoxes[0].GetComponent<TeleType>().Initiate();
        textBoxes[1].GetComponent<TeleType>().Initiate();
        GetComponent<SpeakerSelect>().StartConversation(textBoxes);
    }
}
