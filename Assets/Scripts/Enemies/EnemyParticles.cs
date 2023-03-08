using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticles : MonoBehaviour
{
    public ParticleSystem pSystem;

    public void Explode()
    {
        pSystem.Play();
    }
}
