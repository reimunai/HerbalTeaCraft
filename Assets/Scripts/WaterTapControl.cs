using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public class WaterTapControl : MonoBehaviour
{
    [Header("引导相关参数")] 
    [SerializeField] private bool isNewPlay;
    [SerializeField] private int stepAfterAddWater;
    [SerializeField] private HUDControl hudControl;
    
    [Header("WaterTap")]
    // Start is called before the first frame update
    [SerializeField] private PotManager potManager;
    [SerializeField] private XRSocketInteractor potSocker;
    [SerializeField] private XRSimpleInteractable tap;
    
    [SerializeField] private WaterTapVisualManager waterTapVisualManager;
    
    public bool isTapOpen = false;
    [SerializeField]private float waterFlowSpeed = 0.05f;
    
    [SerializeField] public UnityEvent<float> OnWaterAdd = new UnityEvent<float>();

    private void Awake()
    {
        hudControl = FindObjectOfType<HUDControl>();
    }

    void Start()
    {
        potSocker.selectEntered?.AddListener(OnPotSockerEntered);
        potSocker.selectExited?.AddListener(OnPotSockerEixted);
        tap.selectEntered?.AddListener(OnTapWaterTap);
    }

    private void OnTapWaterTap(SelectEnterEventArgs arg0)
    {
        Debug.Log("Tap water");
        if (!isNewPlay)
        {
            hudControl.ShowStepGrade(stepAfterAddWater);
            isNewPlay = false;
        }
        isTapOpen = !isTapOpen;
        waterTapVisualManager.PlayOrStop(isTapOpen);
    }

    private void Update()
    {
        if (isTapOpen)
        {
            if (potManager)
            {
                potManager.OnAddWater(waterFlowSpeed * Time.deltaTime);
                OnWaterAdd?.Invoke(potManager.pot.waterLevel);
            }
        }
    }

    private void OnPotSockerEntered(SelectEnterEventArgs args)
    {
        var potM = args.interactableObject.transform.GetComponent<PotManager>();
        if (potM)
        {
            potManager = potM;
            OnWaterAdd?.Invoke(potManager.pot.waterLevel);
        }
    }

    private void OnPotSockerEixted(SelectExitEventArgs args)
    {
        Debug.Log(args.interactableObject.transform.name);
        if (potManager)
        {
            potManager = null;
            OnWaterAdd?.Invoke(0f);
        }
    }
    
}
