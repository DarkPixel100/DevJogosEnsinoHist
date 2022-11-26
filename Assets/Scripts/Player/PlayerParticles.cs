using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    public ParticleSystem pSystem;

    public void CreateDust()
    {
        pSystem.Play();
    }
}
