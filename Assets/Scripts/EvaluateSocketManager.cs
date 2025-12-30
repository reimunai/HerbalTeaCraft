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
        socket.selectExited.AddListener(OnExited);
        socket.selectEntered.AddListener(OnSelected);

        control = FindFirstObjectByType<HUDControl>();
    }

    private void OnExited(SelectExitEventArgs arg0)
    {
        control.ClearAll();
    }

    private void OnSelected(SelectEnterEventArgs arg0)
    {
        if (arg0.interactableObject.transform.GetComponent<PotManager>() == potManager)
        {
            control.ClearAll();

            pot = potManager.pot;
            Score = pot.qualityColor;
            control.ShowScoreText((1f-Math.Abs(1f - Score)) * 100);
             
            calculateSimilarity.CalculateWithAll(pot.ingredientsAndWeighs);
        }
        else 
        {
            Debug.Log("·Å¹øsb");
        }
    }
}
