using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for any light object that the player can grab and interact with
/// </summary>
public class LightsGrabbed : GrabbedObjectScript
{
    [Header("Light Object parameters")]
    [Tooltip("Is On or Off, only for Start")]
    public bool on_off;
    [Tooltip("Color of the light.")]
    public GameData.ColorSelection selectedColor;
    [Tooltip("Type of light. Changes the intensity, radius and range.")]
    public GameData.LightType lightType;

    // Color of the light from the selectedColor
    private Color _actualColor;
    // The light GameObject
    private GameObject _actualLight;

    private void Start()
    {

        // Get the selected color
        _actualColor = GameData.GetColor(selectedColor);

        // Get the light and changes it's color
        foreach(Transform child in transform)
        {
            if(child.GetComponent<Light>() != null)
            {
                _actualLight = child.gameObject;
                _actualLight.GetComponent<Light>().color = _actualColor;
                break;
            }
        }

        // On or Off the light
        base.SetIsActive(on_off);
        if (_actualLight != null)
            _actualLight.SetActive(base.GetIsActive());

        // Load all the parameters for the Light Type
        if (lightType == GameData.LightType.Torchlight || lightType == GameData.LightType.Spotlight)
        {
            if (_actualLight.GetComponent<Light>() != null)
            {
                _actualLight.GetComponent<Light>().color = _actualColor;
                _actualLight.GetComponent<Light>().range = GameData.GetLightRange(lightType);
                _actualLight.GetComponent<Light>().intensity = GameData.GetLightIntensity(lightType);
                _actualLight.GetComponent<Light>().innerSpotAngle = GameData.GetLightInnerAngle(lightType);
                _actualLight.GetComponent<Light>().spotAngle = GameData.GetLightAngle(lightType);
            }
        }
        else // Laser
        {
            if (_actualLight.GetComponent<LineRenderer>() != null)
                _actualLight.GetComponent<LineRenderer>().material.color = _actualColor;
        }
    }

    public void SwitchLight()
    {
        SetIsActive(!GetIsActive()); // on_off = !on_off; Inverse the parameter
        if (_actualLight != null)
            _actualLight.SetActive(GetIsActive());

        Debug.Log(" -- Allume la lampe: "+GetIsActive());
    }

    public override void Activate()
    {
        base.Activate();
        SwitchLight();
    }
}
