using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTapVisualManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private ParticleSystem particle;
    void Start()
    {
        particle.Stop();
    }

    public void PlayOrStop(bool value)
    {
        if (value)
        {
            particle.Play();
        }
        else
        {
            particle.Stop();
        }
    }
}
