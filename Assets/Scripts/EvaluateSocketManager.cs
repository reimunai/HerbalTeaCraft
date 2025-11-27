using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EvaluateSocketManager : MonoBehaviour
{
    public XRSocketInteractor socket;
    public CalculateSimilarity calculateSimilarity;
    public PotManager potManager;

    private BrewingPot pot;
    
    private void Start()
    {
        socket.selectEntered.AddListener(OnSelected);
        pot = potManager.pot;
    }

    private void OnSelected(SelectEnterEventArgs arg0)
    {
        if (arg0.interactableObject.transform.GetComponent<PotManager>() == potManager)
        {
            calculateSimilarity.CalculateWithAll(pot.ingredientsAndWeighs);
        }
        else 
        {
            Debug.Log("·Å¹øsb");
        }
    }
}
