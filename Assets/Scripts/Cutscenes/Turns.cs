using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turns : MonoBehaviour
{
    // Mantém qual é o turno atual
    public int currentTurn = 0;

    public GameObject[] facePlates;
    void Start()
    {
        facePlates = GameObject.FindGameObjectsWithTag("Faceplate");
        Check();
    }

    // Verifica se o nome deve ser revelado ou escondido
    public void Check()
    {
        facePlates[0].GetComponent<RevealName>().checkReveal(currentTurn);
        facePlates[1].GetComponent<RevealName>().checkReveal(currentTurn);
        facePlates[0].GetComponent<RevealName>().checkVanish(currentTurn);
        facePlates[1].GetComponent<RevealName>().checkVanish(currentTurn);
    }
}
