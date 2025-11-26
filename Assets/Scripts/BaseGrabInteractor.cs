using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BaseScaleInteractor : XRGrabInteractable
{
    public SocketManager SocketManager;

    private Vector3 originPosition;


    private void Start()
    {
        originPosition = transform.position;
    }

    public void Transport()
    {
        transform.position = originPosition;
    }

}
