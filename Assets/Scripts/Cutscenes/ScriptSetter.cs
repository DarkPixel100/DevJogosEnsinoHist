using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptSetter : MonoBehaviour
{
    // Lê arquivos de texto e coloca o conteúdo adequadamente

    // Variável para inserir o texto importado do arquivo
    private TextAsset scriptAsset;

    // Determinantes para referenciar o arquivo
    private string path;
    private string scName;

    // Linhas de diálogo separadas em um vetor
    public string[] linesArray;

    // Referenciando caixas de texto como objetos
    [HideInInspector]
    public GameObject[] textBoxes;
    void Start()
    {
        textBoxes = GameObject.FindGameObjectsWithTag("Dialogue"); // Referenciando objetos da cena
        string path = SceneUtility.GetScenePathByBuildIndex(SceneManager.GetActiveScene().buildIndex); // Obtendo caminho da cena atual
        string scName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1); // Obtendo o nome da cena atual
        scriptAsset = Resources.Load<TextAsset>("Dialogue" + scName.Substring(8)); // Carregando o texo do script correspondente à cena atual

        string script = scriptAsset.text;

        // Adicionando marcadores personalizados nas quebras de linha
        script = script.Replace("\r\n1:", "<switch><page>").Replace("\r\n2:", "<switch><page>").Replace("\r\n", "<page>").Replace("<switch><page>", "<switch><page>\r\n").Replace("1:", "<line-indent=1em>");
        script = script.Substring(0, script.IndexOf("\r\n") + 2) + "<line-indent=1em>" + script.Substring(script.IndexOf("\r\n") + 2);

        linesArray = script.Split("\r\n"); // Separando as linhas de diálogo em um vetor

        for (int i = 0; i < linesArray.Length; i++)
        {
            if(linesArray.Length >= 2 && i == linesArray.Length - 2)
            {
                linesArray[i] = linesArray[i].Substring(0, linesArray[i].LastIndexOf("<page>")); // Remove o marcador <page> da antepenúltima linha
            }
            textBoxes[i % 2].GetComponent<TMPro.TMP_Text>().text += linesArray[i]; // Adicionando texto selecionado ao texto das caixas de texto
        }

        // Iniciando o teletype de ambas as caixas de texto
        textBoxes[0].GetComponent<TeleType>().Initiate();
        textBoxes[1].GetComponent<TeleType>().Initiate();
    }
}
