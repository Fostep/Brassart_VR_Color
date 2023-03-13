using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class GetHandName : MonoBehaviour
{
    private string _handName; // String name of the hand

    public List<GameObject> _interactableObjects = new List<GameObject>(); // List of GameObject hovered by the hand

    public string interactableLayer; // Layer interectable by the hand

    private GameObject actualObjectInHand;

    public InputActionProperty grabButton;

    public XRDirectInteractor rHand;

    private void Start()
    {
        // Set the name of the hand
        _handName = this.gameObject.name;
        //Debug.Log(_handName);

        if (interactableLayer == null)
            interactableLayer = "Interectable";
    }

    private void Update()
    {
        Debug.Log(this.gameObject.GetComponent<XRDirectInteractor>().selectTarget);
        // If the hand has an object
        if(actualObjectInHand != null && grabButton.action.ReadValue<float>() < 0.1f)
        {

                // If the object has the right hand
                if (actualObjectInHand.GetComponent<HandDetection>().GetCurrentHand() == this.gameObject)
                {
                    // If actual object isnt selected anymore
                    if (!actualObjectInHand.GetComponent<HandDetection>().GetSelectedByHand())
                    {
                        // Clear the hand and the objects saves
                        actualObjectInHand.GetComponent<HandDetection>().SetCurrentHand(null);
                        actualObjectInHand = null;
                    }
                }
        }
        else
        {
            // Look for the whole list
            foreach(GameObject go in _interactableObjects)
            {
                // If the object is selected (grabbed)
                if (go.GetComponent<HandDetection>().GetSelectedByHand())
                {
                    // Hand save object
                    actualObjectInHand = go;
                    // Object save hand
                    actualObjectInHand.GetComponent<HandDetection>().SetCurrentHand(this.gameObject);
                }
                break;
            }
        }
        //Debug.Log(_handName + " : Object is " + actualObjectInHand);
        //Debug.Log(_handName + " : "+grabButton.action.ReadValue<float>());
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(interactableLayer))
        {
            // Can be detected by hand
            if(other.gameObject.GetComponent<HandDetection>() != null)
            {
                // If it hasnt a hand
                if (other.gameObject.GetComponent<HandDetection>().GetCurrentHand() == null)
                {
                    // If not in list of the hand objects
                    if (!_interactableObjects.Contains(other.gameObject))
                    {
                        // Add it
                        _interactableObjects.Add(other.gameObject);
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(interactableLayer))
        {
            if(other == actualObjectInHand)
            {
                // Clear the hand object
                actualObjectInHand = null;
                // Clear the object hand
                other.GetComponent<HandDetection>().SetCurrentHand(null);
            }
            
            // Remove the object from the list 
            if (_interactableObjects.Contains(other.gameObject))
                _interactableObjects.Remove(other.gameObject);
            
        }
    }
}
