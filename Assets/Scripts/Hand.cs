using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Hand : MonoBehaviour
{
    private XRDirectInteractor _handInteractor; // The directInteractor of the hand
    private GameObject _grabbedObject;

    // Inputs 
    public InputActionProperty selectButton;
    public InputActionProperty activateButton;

    private void Start()
    {
        _handInteractor = GetComponent<XRDirectInteractor>();
    }

    private void Update()
    {

    }

    public void SetGrabbedObject(GameObject go)
    {
        _grabbedObject = go;
    }

    public GameObject GetGrabbedObject()
    {
        return _grabbedObject;
    }

    public bool GetSelectButton()
    {
        if(selectButton.action.ReadValue<float>() > 0.1f)
        {
            return true;
        }
        return false;
    }

    public bool GetActivateButton()
    {
        if (activateButton.action.ReadValue<float>() > 0.1f)
        {
            return true;
        }
        return false;
    }
}
