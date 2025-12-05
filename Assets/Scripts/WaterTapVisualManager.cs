using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WaterTapVisualManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private ParticleSystem particle;
    [SerializeField]private VisualEffect visualEffect;
    void Start()
    {
        particle.Stop();
        visualEffect.Stop();
    }

    public void PlayOrStop(bool value)
    {
        if (value)
        {
            particle.Play();
            visualEffect.Play();
        }
        else
        {
            particle.Stop();
            visualEffect.Stop();
        }
    }
}
