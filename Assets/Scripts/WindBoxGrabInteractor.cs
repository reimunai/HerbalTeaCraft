using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WindBoxGrabInteractor : XRGrabInteractable
{
    public float pullSpeed = 0f;
    [SerializeField]private float ZMinLimit = 0f;
    [SerializeField]private float ZMaxLimit = -1f;
    [SerializeField]private bool isGrabbed = false;

    private float _prePosZ;
    private Transform _windBoxHandleTransform;
    private Vector3 _originalPos;
    private Transform _parentTransform;

    private void Start()
    {
        _prePosZ = transform.localPosition.z;
        _originalPos = transform.localPosition;
        _parentTransform = transform.parent.transform;
        _windBoxHandleTransform = transform;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        isGrabbed = true;
        transform.SetParent(_parentTransform);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        isGrabbed = false;
        //transform.localPosition = originalPos;
    }

    private void Update()
    {
        if (isGrabbed)
        {
            transform.localPosition = new Vector3(_originalPos.x, _originalPos.y, transform.localPosition.z);
            if (transform.localPosition.z < _originalPos.z + ZMaxLimit)
            {
                transform.localPosition = new Vector3(_originalPos.x, _originalPos.y, ZMaxLimit + _originalPos.z);

            }
            else if (transform.localPosition.z > _originalPos.z - ZMinLimit)
            {
                transform.localPosition = new Vector3(_originalPos.x, _originalPos.y, ZMinLimit - _originalPos.z);
            }
            //z轴向内部
            float deltaZ = transform.localPosition.z - _prePosZ;
            if (deltaZ < 0.00001f)
            {
                pullSpeed = 0f;
            }
            else
            {
                pullSpeed = (transform.localPosition.z - _prePosZ) / Time.deltaTime;
            }
            Debug.Log(pullSpeed);
            _prePosZ = transform.localPosition.z;
        }
    }
}
