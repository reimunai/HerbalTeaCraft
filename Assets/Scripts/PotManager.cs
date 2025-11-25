using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PotManager : MonoBehaviour
{
    public BrewingPot pot;
    public float potTemperatureDecreseSpeed = 5f;
    public WindBoxGrabInteractor windBox;
    
    public XRSocketInteractor XRSocketInteractor;
    public Ingredient ingredient1;
    // Start is called before the first frame update
    void Start()
    {
        pot.AddIngredient(ingredient1, 20);
        XRSocketInteractor.selectEntered.AddListener((SelectEnterEventArgs args)=>Debug.Log("aaa"));
    }

    private void Update()
    {
        pot.UpdateQualityColor(Time.deltaTime);
        if (pot.isHeating)
        {
            pot.AdjustTemperature(pot.temperature - potTemperatureDecreseSpeed * Time.deltaTime + windBox.pullSpeed * 0.1f);
        }
    }

    public void OnStartHeatingBtn(SelectEnterEventArgs args)
    {
        Debug.Log("OnStartHeatingBtn");
        pot.StartHeating();
        pot.temperature = 0;
    }
}
