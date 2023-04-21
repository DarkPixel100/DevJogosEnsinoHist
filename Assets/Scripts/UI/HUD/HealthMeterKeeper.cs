using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMeterKeeper : MonoBehaviour
{
    public GameObject HeartPrefab;
    public GameObject player;
    public List<GameObject> Children;

    void Start()
    {
        foreach (Transform child in transform) // Percorre os filhos do elemento pai
        {
            if (child) Children.Add(child.gameObject); // Add filhos do elemento pai na lista
        }
        player.GetComponent<HealthNDeath>().health = Children.Count; // Define a vida do personagem
        UpdateHealth(player.GetComponent<HealthNDeath>().health); // Inicia a vida
    }

    /*Ignore (Posso mudar fazendo Instantiate, mas usando uma variável para guardar a posição do último coração instanciado (inicia como a posição do meter),
    e adicionando o spacing + heartWidth.)*/
    public void UpdateHealth(int playerHealth) // Atualiza a vida
    {
        int index = 0;
        foreach (GameObject child in Children) // Percorre a lista de filhos
        {
            if (index < playerHealth)
            {
                child.SetActive(true); // Ativa os filhos
                child.GetComponent<Animator>().Play("HeartBlink"); // Inicia a animação de piscar
                index++;
            }
            else
            {
                child.SetActive(false);
            }
        }
    }
}
