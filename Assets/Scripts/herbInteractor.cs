using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class herbInteractor : XRGrabInteractable
{
    public string IngredientName;
    public float weight;
    public Vector3 originPosition;
    public Vector3 originRotation;
    private void Start()
    {
        originPosition = transform.position;
    }
    //松手艹自动复位
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

            Transport();
    }
    public void Transport() 
    {
        var rb=GetComponent<Rigidbody>();
        if (rb != null)
        {
            // 只有非运动学刚体才能设置速度
            if (!rb.isKinematic)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            else
            {
                // 对于运动学刚体，直接设置位置和旋转
                rb.position = originPosition;
            }
        }
        transform.position = originPosition;
    }
}
