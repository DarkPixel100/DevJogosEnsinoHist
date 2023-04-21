using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPos;
    public Camera cam;
    public float pEffect;
    // Efeito parallax para o background em relação à camera
    void Start()
    {
        // Determinando posição inicial da câmera e comprimento horizontal do background
        startPos = cam.transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Aplicando efeito e fazendo com que a imagem se repita quando passar de alguma extremidade
        float temp = cam.transform.position.x * (1 - pEffect);
        float dist = cam.transform.position.x * pEffect;
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
        if (temp > startPos + length) startPos += length;
        else if (temp < startPos - length) startPos -= length;
    }
}
