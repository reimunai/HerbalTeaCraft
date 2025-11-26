using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;


public class WindBoxControl : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor potSocker;

    [SerializeField] private WindBoxGrabInteractor windBoxHandle;

    [SerializeField] private PotManager potManager;

    [SerializeField] private XRSimpleInteractable startHeatingBtn;
    
    [SerializeField] private XRSimpleInteractable coolingBtn;
    
    public UnityEvent<float> onPotTemperatureChanged = new UnityEvent<float>();
    public UnityEvent<float> onPotQualityColorChanged = new UnityEvent<float>();
    
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
            onPotQualityColorChanged?.Invoke(potManager.pot.qualityColor / 2f);
        }
    }

    private void OnCoolingBtn(SelectEnterEventArgs arg0)
    {
        if(!potManager){return;}
        
        potManager.OnCoolingTemperture();
    }

    private void OnStartHeatingBtn(SelectEnterEventArgs arg0)
    {
        if (!potManager)
        {
            Debug.Log("No Pot for Heating");
            return;
        }
        
        potManager.OnStartHeatingBtn();
    }
    
    

    private void OnPotSockerEntered(SelectEnterEventArgs args)
    {
        var potM = args.interactableObject.transform.GetComponent<PotManager>();
        if (potM)
        {
            potManager = potM;
            potManager.windBox = windBoxHandle;
        }
    }

    private void OnPotSockerEixted(SelectExitEventArgs args)
    {
        Debug.Log(args.interactableObject.transform.name);
        if (potManager)
        {
            potManager.windBox = null;
            potManager.pot.isHeating = false;
            potManager = null;
        }
    }
}
