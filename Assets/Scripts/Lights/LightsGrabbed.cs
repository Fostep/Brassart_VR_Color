using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsGrabbed : GrabbedObjectScript
{
    [Header("Object parameters")]
    public bool on_off;
    public GameData.ColorSelection selectedColor;
    private Color actualColor;

    private GameObject _actualLight;

    private void Start()
    {
        // Get the light 
        foreach(Transform child in transform)
        {
            if(child.GetComponent<Light>() != null)
            {
                _actualLight = child.gameObject;
                break;
            }
        }

        if (_actualLight != null)
            _actualLight.SetActive(on_off);

        actualColor = GameData.GetColor(selectedColor);
    }

    public void SwitchLight()
    {
        on_off = !on_off;
        if(_actualLight != null)
            _actualLight.SetActive(on_off);
    }

    public override void Activate()
    {
        base.Activate();
        SwitchLight();
    }
}
