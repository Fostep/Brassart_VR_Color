using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightObject : MonoBehaviour
{
    // Parameters
    private GameObject _lightObject; // The game object with the light component

    public GameData.LightType lightType; // The type of light, laser, spot or torchlight
    public GameData.ColorSelection colorSelection; // The color selected

    private Color _actualColor; // Color of light

    public bool isActive; // Is on or off

    // Start is called before the first frame update
    void Start()
    {
        _lightObject = this.transform.Find("Spot Light").gameObject;

        _lightObject.SetActive(isActive);

        _actualColor = GameData.GetColor(colorSelection);

        if (lightType == GameData.LightType.Torchlight || lightType == GameData.LightType.Spotlight)
        {
            if (_lightObject.GetComponent<Light>() != null)
                _lightObject.GetComponent<Light>().color = _actualColor;
        }
        else // Laser
        {
            if (_lightObject.GetComponent<LineRenderer>() != null)
                _lightObject.GetComponent<LineRenderer>().material.color = _actualColor;
        }
    }

    /// <summary>
    /// Switch the boolean controling if the light is on or not
    /// Change the source active
    /// </summary>
    public void SwitchLight()
    {
        isActive = !isActive;
        if (_lightObject != null)
        {
            _lightObject.SetActive(isActive);
        }
    }

    /// <summary>
    /// General function to activate when the player press a button with the object 
    /// in hand or by another mean.
    /// </summary>
    public void Activate()
    {
        SwitchLight();
    }
}
