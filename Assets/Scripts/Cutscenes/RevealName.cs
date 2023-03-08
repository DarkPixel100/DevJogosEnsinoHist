using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevealName : MonoBehaviour
{
    public int revealTurn;
    public int vanishTurn;

    public Sprite face;

    private bool revealed;

    public void checkReveal(int currentTurn)
    {
        if(!revealed && currentTurn >= revealTurn)
        {
            if (face) {
                DestroyImmediate(GetComponent<TMPro.TMP_Text>());
                GetComponent<RectTransform>().sizeDelta = new Vector2(190, 220);
                gameObject.AddComponent<Image>().sprite = face;
            } else if (GetComponent<TMPro.TMP_Text>().text == "?") GetComponent<TMPro.TMP_Text>().text = "Texture Missing";
            revealed = true;
        }
    }

    public void checkVanish(int currentTurn)
    {
        if(vanishTurn != 0 && currentTurn >= vanishTurn)
        {
            gameObject.SetActive(false);
        }
    }
}
