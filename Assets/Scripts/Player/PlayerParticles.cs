using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    public ParticleSystem jumpParticles;
    public ParticleSystem expParticles;

    public void CreateDust() // Cria fumaça ao pular
    {
        jumpParticles.Play();
    }

    public void Explode() // Cria outra fumaça ao morrer
    {
        expParticles.Play();
    }
}
