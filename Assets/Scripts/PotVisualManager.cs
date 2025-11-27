using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotVisualManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem.Stop();
    }

    public void OnBoiling()
    {
        _particleSystem.Play();
    }
}
