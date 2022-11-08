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
        StartCoroutine(destroyProj(destroyTime));
    }

    void Update()
    {
        transform.Rotate(0, 0, rotationFactor * rotationDirection);
    }

    IEnumerator destroyProj(float t)
    {
        yield return new WaitForSeconds(t);
        GameObject.Destroy(gameObject);
    }
}
