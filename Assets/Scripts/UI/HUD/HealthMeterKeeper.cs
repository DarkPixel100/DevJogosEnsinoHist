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
        foreach (Transform child in transform)
        {
            if (child)
            {
                Children.Add(child.gameObject);
            }
        }
        player.GetComponent<HealthNDeath>().health = Children.Count;
        UpdateHealth(player.GetComponent<HealthNDeath>().health);
    }

    public void UpdateHealth(int playerHealth) //Posso mudar fazendo Instantiate, mas usando uma variável para guardar a posição do último coração instanciado (inicia como a posição do meter), e adicionando o spacing + heartWidth.
    {
        int index = 0;
        foreach (GameObject child in Children)
        {
            if (index < playerHealth)
            {
                child.SetActive(true);
                index++;
            }
            else
            {
                child.SetActive(false);
            }
        }
    }
}
