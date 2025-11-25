
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketManager : MonoBehaviour
{
    public List<XRSocketInteractor> sockets;

    private Ingredient IngredientName;
    private float totalWeight = 0f;
    private List<Transform> objInSocket=new List<Transform>();
    private void Start()
    {

        foreach (var socket in sockets)
        {
            socket.selectEntered.AddListener(OnInserted);
            socket.selectExited.AddListener(OnRemoved);
            Debug.Log("111");
        }
    }
    private void OnInserted(SelectEnterEventArgs arg0)
    {
        var objTransform = arg0.interactableObject.transform;
        var herb = objTransform.GetComponent<herbInteractor>();
        if (herb.IngredientName == IngredientName)
        {
            if (!objInSocket.Contains(objTransform))
            {
                objInSocket.Add(objTransform);
                objTransform.GetComponent<Collider>().enabled = false;
                totalWeight += herb.weight;
            }
        }
        else
            Debug.Log("只能称量同种艹");
    }
    private void OnRemoved(SelectExitEventArgs arg0)
    {
        var objTransform = arg0.interactableObject.transform;
        if (objInSocket.Contains(objTransform))
        {
            objInSocket.Remove(objTransform);
            objTransform.GetComponent<Collider>().enabled = true;
        }
    }

 
    private void OnGrabbed(SelectEnterEventArgs arg0)
    {
        foreach (var obj in objInSocket)
        { 
            var grab = obj.GetComponent<XRGrabInteractable>();
            if (grab != null) 
            {
                grab.enabled = false;
            }
        }
    }
    private void OnUnGrabbed(SelectExitEventArgs arg0)
    {
        foreach (var obj in objInSocket)
        { 
            var grab = obj.GetComponent<XRGrabInteractable>();
            if (grab != null)
            { 
                grab.enabled = true;
            }
        }
    }


  
}
