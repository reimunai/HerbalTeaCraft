using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;

public class WindBoxVisual : MonoBehaviour
{
    // Start is called before the first frame update
    //module
    public VisualEffect fireVFX;
    public Light fireLight;
    public bool isFiring = false;
    
    public float fireFlashSpeed = 5f;
    public float fireIntensity = 5f;
    private void Start()
    {
        fireVFX.Stop();
        fireLight.enabled = false;
    }

    private void Update()
    {
        if (isFiring)
        {
            fireLight.intensity = Mathf.Sin(Time.time * fireFlashSpeed) * fireIntensity + 2f * fireIntensity;
        }
    }

    public void OnMakeFire()
    {
        fireVFX.Play();
        fireLight.enabled = true;
        isFiring = true;
    }


}
