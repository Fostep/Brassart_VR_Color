using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class HandDetection : MonoBehaviour
{
    private XRGrabInteractable _grabbable;
    private GameObject _currentHand;

    // Return if the object is grabbed or not
    public bool GetSelectedByHand()
    {
        return _grabbable.isSelected;
    }

    public void SetCurrentHand(GameObject handGO)
    {
        _currentHand = handGO;
    }

    public GameObject GetCurrentHand()
    {
        return _currentHand;
    }
    private void Start()
    {
        _grabbable = GetComponent<XRGrabInteractable>();
    }
}
