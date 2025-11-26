
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketManager : MonoBehaviour
{
    public List<XRSocketInteractor> sockets;

    public string IngredientName = null;
   
    public float totalWeight = 0f;

    private List<Transform> objInSocket=new List<Transform>();
    
    private void Start()
    {
        foreach (var socket in sockets)
        {
            socket.selectEntered.AddListener(OnInserted);
            socket.selectExited.AddListener(OnRemoved);
        }
    }
    private void OnInserted(SelectEnterEventArgs arg0)
    {
        var objTransform = arg0.interactableObject.transform;
        var herb = objTransform.GetComponent<herbInteractor>();

        herb.isInSocket = true;

        if (IngredientName != null)
        {
            if (herb.IngredientName == IngredientName)
            {
                if (!objInSocket.Contains(objTransform))
                {
                    objInSocket.Add(objTransform);
               
                    objTransform.GetComponent<Collider>().enabled = false;
                    
                    totalWeight += herb.weight;
                }
                //让上一个物体不可抓，仅最后一个物体可抓
                Debug.Log(objInSocket.Count+" in times ");
                if (objInSocket.Count >= 1)
                {
                    objInSocket[objInSocket.Count - 1].GetComponent<Collider>().enabled = true;
                }
            }
            else
            {
                // 若非同种艹吸附到插槽内，找到容纳其插槽，并将该艹移除
                foreach (var socket in sockets)
                {
                    if (socket.firstInteractableSelected == arg0.interactableObject)
                    {
                        socket.interactionManager.SelectExit(socket, arg0.interactableObject);
                        Debug.Log("只能称量同种艹");
                    }
                }
                
            }
        }
        else
        {
            IngredientName = herb.IngredientName;
           
            objInSocket.Add(objTransform);
            totalWeight += herb.weight;
           
            Debug.Log(herb.IngredientName+" is now");
        }
    }
    private void OnRemoved(SelectExitEventArgs arg0)
    {
        var objTransform = arg0.interactableObject.transform;
        var obj = arg0.interactableObject.transform.gameObject;
        var herb = objTransform.GetComponent<herbInteractor>();

        herb.isInSocket = false;

        if (objInSocket.Contains(objTransform))
        {
            Debug.Log("leave");
            objInSocket.Remove(objTransform);
            
            objTransform.GetComponent<Collider>().enabled = true;
            totalWeight -= herb.weight;
            
        }
        Debug.Log(objInSocket.Count + " out times ");
        if (objInSocket.Count >= 1)
        {
            objInSocket[objInSocket.Count - 1].GetComponent<Collider>().enabled = true;
        }
        ////艹移除时令艹归位
        //obj.transform.position = herb.originTransform;


        //若无艹，数据归零
        foreach (var socket in sockets)
        {
            if (socket.hasSelection)
            {
                return;
            }
        }
        IngredientName = null;
        totalWeight = 0;
        Debug.Log("nothing is now");
    }
}
