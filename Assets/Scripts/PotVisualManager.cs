using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PotVisualManager : MonoBehaviour
{
    [SerializeField] private VisualEffect _visualEffect;

    private void Awake()
    {
        _visualEffect.Stop();
    }

    public void OnBoiling()
    {
        _visualEffect.Play();
    }
}
