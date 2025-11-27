using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PotManager : MonoBehaviour
{
    [SerializeField] private Color normalColor;
    [SerializeField] private Color bestColor;
    [SerializeField] private Color overFireColor;
    
    private MeshRenderer _meshRenderer;
    
    public BrewingPot pot;
    public float potTemperatureDecreseSpeed = 5f;
    public WindBoxGrabInteractor windBox;
    
    // Start is called before the first frame update
    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        pot.UpdateQualityColor(Time.deltaTime);
        if (pot.isHeating)
        {
            pot.AdjustTemperature(pot.temperature - potTemperatureDecreseSpeed * Time.deltaTime + windBox.pullSpeed * 0.1f);
            ChangePotColor();
        }
    }

    public void OnStartHeatingBtn()
    {
        Debug.Log("OnStartHeatingBtn");
        pot.StartHeating();
        pot.temperature = 0;
    }

    public void OnCoolingTemperture()
    {
        pot.AdjustTemperature(pot.temperature - 30f);
    }
    
    public void ChangePotColor()
    {
        Color color = normalColor;
        if (pot.qualityColor <= 1)
        {
            color = UnityEngine.Color.Lerp(normalColor, bestColor, pot.qualityColor);
        }
        else
        {
            var t = pot.qualityColor - 1f;
            color = Color.Lerp(bestColor, overFireColor, t);
        }
        
        _meshRenderer.material.color = color;
    }
    
    
}