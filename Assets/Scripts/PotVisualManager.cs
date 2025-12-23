using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PotVisualManager : MonoBehaviour
{
    [SerializeField] private VisualEffect _visualEffect;
    public MeshRenderer waterSurfaceRenderer;
    private void Awake()
    {
        _visualEffect.Stop();
    }

    private void Start()
    {
        if (waterSurfaceRenderer != null)
        {
            waterSurfaceRenderer.material = Instantiate(waterSurfaceRenderer.material);
        }
    }

    public void OnBoiling()
    {
        _visualEffect.Play();
    }
    
    public void ChangeMaterialColor(Color color)
    {
        waterSurfaceRenderer.material.SetColor("_DepthColor1", color);
    }
}
