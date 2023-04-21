using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevealName : MonoBehaviour
{
    // "Turnos" para que o faceplace seja revelado e escondido
    public int revealTurn;
    public int vanishTurn;
    // Sprite da faceplate
    public Sprite face;
    // Verificando se já foi revelado
    private bool revealed;

    // Checando se deve revelar a faceplate
    public void checkReveal(int currentTurn)
    {
        if(!revealed && currentTurn >= revealTurn)
        {
            if (face) {
                // Trocando a "?" pela imagem
                DestroyImmediate(GetComponent<TMPro.TMP_Text>());
                GetComponent<RectTransform>().sizeDelta = new Vector2(190, 220);
                gameObject.AddComponent<Image>().sprite = face;
            } else if (GetComponent<TMPro.TMP_Text>().text == "?") GetComponent<TMPro.TMP_Text>().text = "Texture Missing";
            revealed = true; // Já revelado
        }
    }

    // Checando se deve esconder a faceplate
    public void checkVanish(int currentTurn)
    {
        if(vanishTurn != 0 && currentTurn >= vanishTurn)
        {
            gameObject.SetActive(false);
        }
    }
}
