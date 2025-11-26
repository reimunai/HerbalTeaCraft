using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WaterTapControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private PotManager potManager;
    [SerializeField] private XRSocketInteractor potSocker;
    
    [SerializeField]private bool isTapOpen = false;
    [SerializeField]private float waterFlowSpeed = 0.05f;
    void Start()
    {
        potSocker.selectEntered?.AddListener(OnPotSockerEntered);
        potSocker.selectExited?.AddListener(OnPotSockerEixted);
    }

    private void Update()
    {
        if (isTapOpen)
        {
            if (potManager)
            {
                potManager.pot.AddWater(waterFlowSpeed * Time.deltaTime);
            }
        }
    }

    private void OnPotSockerEntered(SelectEnterEventArgs args)
    {
        var potM = args.interactableObject.transform.GetComponent<PotManager>();
        if (potM)
        {
            potManager = potM;
        }
    }

    private void OnPotSockerEixted(SelectExitEventArgs args)
    {
        Debug.Log(args.interactableObject.transform.name);
        if (potManager)
        {
            potManager = null;
        }
    }
    
}
