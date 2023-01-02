using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerSelect : MonoBehaviour
{
    public int currentSpeaker;

    private GameObject[] textBoxes;

    public GameObject sceneManager;

    public void StartConversation()
    {
        textBoxes = GameObject.FindGameObjectsWithTag("Dialogue");
        textBoxes[currentSpeaker].GetComponent<TeleType>().Speak(true);
    }

    public void SwitchSpeaker()
    {
        currentSpeaker = (currentSpeaker + 1) % 2;
        textBoxes[currentSpeaker].GetComponent<TeleType>().currentPiece++;
        textBoxes[currentSpeaker].GetComponent<TeleType>().Speak(false);
    }

    public void EndCutscene()
    {
        sceneManager.GetComponent<SceneManage>().ChangeScene("NextLevel");
    }
}
