using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer; // Mixer de áudio do jogo

    public Toggle fsToggle; // Toggle da tela cheia

    public Slider volumeSlider; // Barra de volume

    public TMPro.TMP_Dropdown resolutionDropdown; // Dropdown de resoluções
    Resolution[] resolutions;

    void Start()
    {
        // Volume
        volumeSlider.value = PlayerPrefs.GetFloat("GameVolume"); // Define o valor como o mesmo salvo nas preferências
        audioMixer.SetFloat("Volume", volumeSlider.value); // Define o volume do mixer como o valor do volume, em dB

        // Tela cheia
        fsToggle.isOn = Screen.fullScreen;

        // Resolução
        resolutions = Screen.resolutions; // Pega as resoluções possíveis
        resolutionDropdown.ClearOptions(); // Limpa a lista do elemento dropdown
        List<string> options = new List<string>(); // Cria uma nova lista
        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + ": " + resolutions[i].refreshRateRatio + "Hz";
            options.Add(option); // Add as opções na lista criada

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) currentResIndex = i; // Define o índice da resolução atual
        }
        resolutionDropdown.AddOptions(options); // Define a lista do dropdown como igual a mesma criada
        resolutionDropdown.value = currentResIndex; // Define o valor atual como o índice da resolução atual
        resolutionDropdown.RefreshShownValue(); // Atualiza o valor mostrado no dropdown
        gameObject.SetActive(false);
    }

    public void SetFullscren(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen; // Coloca o jogo em tela-cheia
    }

    public void SetVolume(float volume) // Define o volume do jogo e salva-o nas preferências
    {
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("GameVolume", volume);
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen); // Aplica a resolução
        if(GameObject.Find("Player")) GameObject.Find("Main Camera").GetComponent<CameraMovement>().CamFollow(); // Reajusta a câmera com a resolução nova
    }
}
