using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public class WindBoxControl : MonoBehaviour
{
    [Header("引导相关参数")]
    [SerializeField] private bool isNewPlay = false;
    
    [SerializeField] private int stepAfterPutPot;
    [SerializeField] private int stepAfterHeating;
    [SerializeField] private int bestTimeStep;
    [SerializeField] private int stepAfterEndCook;
    
    [SerializeField] public HUDControl hudControl;
    [Header("相关组件")]
    [SerializeField] private XRSocketInteractor potSocker;

    [SerializeField] private WindBoxGrabInteractor windBoxHandle;

    [SerializeField] private PotManager potManager;

    [SerializeField] private XRSimpleInteractable startHeatingBtn;
    
    [SerializeField] private XRSimpleInteractable coolingBtn;
    
    
    
    public UnityEvent<float> onPotTemperatureChanged = new UnityEvent<float>();
    public UnityEvent<float> onPotQualityColorChanged = new UnityEvent<float>();
    public UnityEvent onCookingEnded = new UnityEvent();
    
    private void Start()
    {
        potSocker.selectEntered?.AddListener(OnPotSockerEntered);
        potSocker.selectExited?.AddListener(OnPotSockerEixted);
        startHeatingBtn.selectEntered?.AddListener(OnStartHeatingBtn);
        //coolingBtn.selectEntered?.AddListener(OnCoolingBtn);
    }

    private void Update()
    {
        if (potManager && potManager.pot.isHeating)
        {
            onPotTemperatureChanged?.Invoke(potManager.pot.temperature);
            onPotQualityColorChanged?.Invoke(potManager.pot.qualityColor);
        }
    }

    private void OnCoolingBtn(SelectEnterEventArgs arg0)
    {
        if(!potManager){return;}
        
        potManager.OnCoolingTemperture();
    }

    private void OnQualityCOlorBest(float value)
    {
        if (value > 0.9f && !isNewPlay)
        {
            hudControl.ShowStepGrade(bestTimeStep);
        }
    }
    
    private void OnStartHeatingBtn(SelectEnterEventArgs arg0)
    {
        if (!potManager)
        {
            Debug.Log("No Pot for Heating");
            return;
        }

        if (!potManager.pot.isHeating)
        {
            if (!isNewPlay)
            {
                hudControl.ShowStepGrade(stepAfterHeating);
            }

            potManager.OnStartHeatingBtn();
        }
        else
        {
            potManager.pot.isHeating = false;
            potManager.pot.timer = 0f;
            onCookingEnded?.Invoke();
        }
    }
    
    

    private void OnPotSockerEntered(SelectEnterEventArgs args)
    {
        var potM = args.interactableObject.transform.GetComponent<PotManager>();
        if (potM)
        {
            potManager = potM;
            potManager.windBox = windBoxHandle;
            if (!isNewPlay)
            {
                hudControl.ShowStepGrade(stepAfterPutPot);
            }
        }
    }

    private void OnPotSockerEixted(SelectExitEventArgs args)
    {
        Debug.Log(args.interactableObject.transform.name);
        if (potManager)
        {
            if (!isNewPlay)
            {
                hudControl.ShowStepGrade(stepAfterEndCook);
                isNewPlay = true;
            }
            potManager.windBox = null;
            potManager.pot.isHeating = false;
            potManager.pot.timer = 0f;
            onCookingEnded?.Invoke();
            potManager = null;
        }
    }
}
