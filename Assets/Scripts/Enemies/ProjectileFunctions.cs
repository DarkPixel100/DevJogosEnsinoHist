using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFunctions : MonoBehaviour
{
    public float destroyTime;
    public float rotationFactor;
    [System.NonSerialized]
    public int rotationDirection;

    void Start()
    {
        StartCoroutine(destroyProj(destroyTime)); // Determina o tempo de destruição do projétil
    }

    void Update()
    {
        transform.Rotate(0, 0, rotationFactor * rotationDirection); // Inicia um movimento de rotação constante
    }

    IEnumerator destroyProj(float t) // Destrói o objeto após um tempo t, em segundos
    {
        yield return new WaitForSeconds(t);
        GameObject.Destroy(gameObject);
    }
}
