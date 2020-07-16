using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSpawnerDeath : MonoBehaviour
{
    public Transform Deathparticles;
    ParticleSystem[] ps;
    public Transform Aliveparticles;
    ParticleSystem[] AlivePs;
    CharacterStats stats;
    private bool isDead;

    private void Start()
    {
        stats = GetComponent<CharacterStats>();
        ps = Deathparticles.GetComponentsInChildren<ParticleSystem>();
        AlivePs = Aliveparticles.GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem ps in ps)
        {
            ps.Stop();
        }
    }


    private void Update()
    {
        if (stats.currentHealth <= 0 && !isDead)
        {
            isDead = true;
            foreach (ParticleSystem ps in ps)
            {
                ps.Play();
            }
            foreach(ParticleSystem ps in AlivePs)
            {
                ps.Stop();
            }
        }
    }
}
