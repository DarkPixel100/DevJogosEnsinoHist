using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealName : MonoBehaviour
{
    public int revealTurn;

    public string realName;

    private bool revealed;

    public void checkReveal(int currentTurn)
    {
        if(!revealed && currentTurn >= revealTurn)
        {
            GetComponent<TMPro.TMP_Text>().text = realName;
            revealed = true;
        }
    }
}
