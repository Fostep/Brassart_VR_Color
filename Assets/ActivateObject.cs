using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ActivateObject : MonoBehaviour
{
    private GameObject _objectModel; // Model 3D
    private GrabbedObjectScript _grab; // Script for grabbing and 
    //private GameObject _colorLight;

    public bool on_off;


    // Start is called before the first frame update
    void Start()
    {
        _grab = GetComponent<GrabbedObjectScript>();

        // The game object containing all the 3D models
        _objectModel = this.transform.Find("Object Model").gameObject;

        //
        //_colorLight = this.transform.Find("Colored Light").gameObject;

        //_colorLight.SetActive(on_off);
    }
    public void Activate()
    {
        Debug.Log("Activate: "+ this.name);
    }

    /*
    public void ActivateLight()
    {
        _colorLight.SetActive(true);
    }

    public void DesactivateLight()
    {
        _colorLight.SetActive(false);
    }

    public void SwitchLight()
    {
        if (_grab.isSelected)
        {
            on_off = !on_off;
            if(_colorLight != null)
                _colorLight.SetActive(on_off);
        }
        
    }
    */
    
}
