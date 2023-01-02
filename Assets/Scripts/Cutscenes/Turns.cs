using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turns : MonoBehaviour
{
    public int currentTurn = 0;

    public GameObject[] facePlates;
    void Start()
    {
        facePlates = GameObject.FindGameObjectsWithTag("Faceplate");
    }

    public void check()
    {
        facePlates[0].GetComponent<RevealName>().checkReveal(currentTurn);
        facePlates[1].GetComponent<RevealName>().checkReveal(currentTurn);
    }
}
