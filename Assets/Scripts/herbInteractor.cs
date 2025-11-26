using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class herbInteractor : XRGrabInteractable
{
    public string IngredientName;
    public float weight;
    public bool isInSocket = false;
    public Vector3 originTransform;
    private void Start()
    {
        originTransform = transform.position;
    }
    //松手艹自动复位
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        if (!isInSocket)
        {
            Transport();
        }
    }
    public void Transport() 
    {
        transform.position = originTransform;
    }
}
