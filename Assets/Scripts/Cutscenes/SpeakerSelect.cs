using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerSelect : MonoBehaviour
{
    // Guarda o valor do falante atual (0 ou 1s)
    public int currentSpeaker;

    private GameObject[] textBoxes;

    public GameObject sceneManager;

    public void StartConversation() // Inicia o diálogo
    {
        textBoxes = GameObject.FindGameObjectsWithTag("Dialogue");
        textBoxes[currentSpeaker].GetComponent<TeleType>().Speak(true); // Inicia a função principal do script teletype
    }

    public void SwitchSpeaker()
    {
        currentSpeaker = (currentSpeaker + 1) % 2; // Troca falante
        textBoxes[currentSpeaker].GetComponent<TeleType>().currentPiece++; // Define o novo "current piece"
        textBoxes[currentSpeaker].GetComponent<TeleType>().Speak(false); // Finaliza o "teletype" do falante atual
    }

    public void EndCutscene()
    {
        sceneManager.GetComponent<SceneManage>().ChangeScene("NextLevel");
    }
}
