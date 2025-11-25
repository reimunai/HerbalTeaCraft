using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PotManager : MonoBehaviour
{
    public BrewingPot pot;

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
    }
}
