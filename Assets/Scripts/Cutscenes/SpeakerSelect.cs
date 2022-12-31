using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerSelect : MonoBehaviour
{
    public int currentSpeaker;

    private GameObject[] boxes;

    public void StartConversation(GameObject[] objArr)
    {
        boxes = objArr;
        boxes[currentSpeaker].GetComponent<TeleType>().Speak(true);
    }

    public void switchSpeaker()
    {
        currentSpeaker = (currentSpeaker + 1) % 2;
        boxes[currentSpeaker].GetComponent<TeleType>().currentPiece++;
        boxes[currentSpeaker].GetComponent<TeleType>().Speak(false);
    }
}
