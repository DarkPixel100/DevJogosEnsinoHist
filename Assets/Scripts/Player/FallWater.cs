using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallWater : MonoBehaviour
{
    [SerializeField] private LayerMask waterLayer;

    private void OnTriggerEnter2D(Collider2D waterTilemap) // Se o personagem cair na água, faz som de algo caindo na água
    {
        if (((1 << waterTilemap.gameObject.layer) & waterLayer) != 0)
        {
            waterTilemap.GetComponent<AudioSource>().Play();
        }
    }
}
