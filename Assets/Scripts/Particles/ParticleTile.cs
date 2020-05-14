using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTile : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public ParticleGenerator generator;

    void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, particleSystem.shape.scale);
    }

    
}
