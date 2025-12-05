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
    public bool isHatClosed = false;
    public Color visualColor = Color.white;
    public BrewingPot pot;
    public float potTemperatureDecreseSpeed = 5f;
    public WindBoxGrabInteractor windBox;
    public XRSocketInteractor hatSocket;
    public XRSocketInteractor scaleSocket;
    
    // Start is called before the first frame update
    private void Start()
    {
        hatSocket.selectEntered?.AddListener(OnHatSocketEntered);
        hatSocket.selectExited?.AddListener(OnHatSocketExited);
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
        visualColor = normalColor;
        if (pot.qualityColor <= 1)
        {
            visualColor = UnityEngine.Color.Lerp(normalColor, bestColor, pot.qualityColor);
        }
        else
        {
            var t = pot.qualityColor - 1f;
            visualColor = Color.Lerp(bestColor, overFireColor, t);
        }
    }

    public void OnAddWater(float water)
    {
        if (isHatClosed)
        {
            return;
        }
        
        pot.AddWater(water);
    }
    
    public void OnHatSocketEntered(SelectEnterEventArgs args)
    {
        Debug.Log("OnHatSocketEntered");
        scaleSocket.enabled = false;
        isHatClosed = true;
    }

    public void OnHatSocketExited(SelectExitEventArgs args)
    {
        Debug.Log("OnHatSocketExited");
        scaleSocket.enabled = true;
        isHatClosed = false;
    }
}