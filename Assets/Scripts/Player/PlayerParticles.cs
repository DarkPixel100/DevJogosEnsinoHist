using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    public ParticleSystem particleSystem;

    public void CreateDust()
    {
        particleSystem.Play();
    }
}
