using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    public ParticleSystem jumpParticles;
    public ParticleSystem expParticles;

    public void CreateDust()
    {
        jumpParticles.Play();
    }

    public void Explode()
    {
        expParticles.Play();
    }
}
