using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ActivateObject : MonoBehaviour
{
    private GameObject _objectModel;
    private GameObject _colorLight;

    public InputActionProperty switchButton;
    public InputActionReference switchButtonBIS;

    private XRGrabInteractable grabbable;

    public bool on_off;
    private string _handName;

    private void Awake()
    {
        switchButtonBIS.action.performed += SwitchLight;
        switchButtonBIS.action.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        grabbable = GetComponent<XRGrabInteractable>();
        //grabbable.activated.AddListener(SwitchLight);
        
        // The game object containing all the 3D models
        _objectModel = this.transform.Find("Object Model").gameObject;

        //
        _colorLight = this.transform.Find("Colored Light").gameObject;

        _colorLight.SetActive(on_off);
    }

    public void ActivateLight()
    {
        _colorLight.SetActive(true);
    }

    public void DesactivateLight()
    {
        _colorLight.SetActive(false);
    }

    public void SwitchLight(/*ActivateEventArgs arg*/InputAction.CallbackContext context)
    {
        /*if (switchButton.action.ReadValue<float>() > 0.1f)
        {
        }*/
        if (grabbable.isSelected)
        {
            on_off = !on_off;
            _colorLight.SetActive(on_off);
        }
        
    }

}
