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
    public HUDControl control;

    private BrewingPot pot;
    private float Score;
    private void Start()
    {
        socket.selectEntered.AddListener(OnSelected);
        pot = potManager.pot;
        Score = pot.qualityColor;
        control = FindFirstObjectByType<HUDControl>();
    }

    private void OnSelected(SelectEnterEventArgs arg0)
    {
        if (arg0.interactableObject.transform.GetComponent<PotManager>() == potManager)
        {
            control.ClearAll();

            control.ShowScoreText((1-Math.Abs(1 - Score)) * 100);
             
            calculateSimilarity.CalculateWithAll(pot.ingredientsAndWeighs);
        }
        else 
        {
            Debug.Log("·Å¹øsb");
        }
    }
}
