using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticleScript : MonoBehaviour
{
    public ParticleSystem deathParticles;

    private void Awake()
    {
        deathParticles.Play();
    }

    private void FixedUpdate()
    {
        if (!deathParticles.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }
}
