using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HolderScript : MonoBehaviour
{
    [Tooltip("The object that is holded.")]
    public GameObject grabObject;

    private Transform _attachPoint;

    // If one of these component than its true
    private bool XRGrab = false;
    private bool GrabbedObject = false;
    private bool LightsGrabbed = false;

    private void Start()
    {
        _attachPoint = transform.Find("Attach Point");

        CheckComponentSelected();

        //Look for the right component
        if (XRGrab || GrabbedObject || LightsGrabbed)
        {
            if(!CheckComponentSelected()) // If not selected then SetPositionAndRotation
                grabObject.transform.SetPositionAndRotation(_attachPoint.position, _attachPoint.rotation);
        }
    }

    private void FixedUpdate()
    {
        if (XRGrab || GrabbedObject || LightsGrabbed)
        {
            if (!CheckComponentSelected()) // If not selected then SetPositionAndRotation
                grabObject.transform.SetPositionAndRotation(_attachPoint.position, _attachPoint.rotation);
            Debug.Log("Attacher");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (grabObject == null) // Redo it
            grabObject = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (grabObject == other.gameObject)
            grabObject = null;
    }

    /// <summary>
    /// Is the object selected
    /// </summary>
    /// <returns></returns>
    private bool CheckComponentSelected()
    {
        if(grabObject != null)
        {
            if (grabObject.GetComponent<XRGrabInteractable>() != null)
            {
                XRGrab = true;
                return grabObject.GetComponent<XRGrabInteractable>().isSelected;
            }
            else if (grabObject.GetComponent<GrabbedObjectScript>() != null)
            {
                GrabbedObject = true;
                return grabObject.GetComponent<GrabbedObjectScript>().isSelected;
            }
            else if (grabObject.GetComponent<LightsGrabbed>() != null)
            {
                LightsGrabbed = true;
                return grabObject.GetComponent<LightsGrabbed>().isSelected;
            }
 
        }
        return false;
    }
}
