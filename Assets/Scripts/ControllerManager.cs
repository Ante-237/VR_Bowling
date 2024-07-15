using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerManager : MonoBehaviour
{
    public InputActionReference RightHand;
    public InputActionReference LeftHand;

    public GameObject RightHandRay;
    public GameObject LeftHandRay;
    public GameObject RightHandDirect;
    public GameObject LeftHandDirect;

    private void Start()
    {
       
    }

    private void Update()
    {
        if (RightHand.action.IsPressed())
        {
            ActivateRayRight();
        }
        else
        {
            ActivateRightDirect();
        }

        if(LeftHand.action.IsPressed())
        {
            ActivateRayLeft();
        }
        else
        {
            ActivateLeftDirect();
        }
    }

    public void ActivateRightDirect()
    {
        RightHandDirect.SetActive(true);
        RightHandRay.SetActive(false);
    }

    public void ActivateLeftDirect()
    {
        LeftHandDirect.SetActive(true);
        LeftHandRay.SetActive(false);
    }

    public void ActivateRayRight()
    {
        RightHandRay.SetActive(true);
        RightHandDirect.SetActive(false);
    }

    public void ActivateRayLeft()
    {
        LeftHandRay.SetActive(true);
        LeftHandDirect.SetActive(false);
    }
}
