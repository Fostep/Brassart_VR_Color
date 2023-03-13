using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ActivateTeleportationRay : MonoBehaviour
{
    public GameObject leftTeleportation;
    public GameObject rightTeleportation;

    public InputActionProperty leftActivate;
    public InputActionProperty rightActivate;

    public InputActionProperty leftCancel;
    public InputActionProperty rightCancel;

    private void Start()
    {
        // Teleportation isn't shown at the beginning 
        leftTeleportation.SetActive(false);
        rightTeleportation.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if(!(leftCancel.action.ReadValue<Vector2>().x > 0.1f || leftCancel.action.ReadValue<Vector2>().x < -0.1f))
            leftTeleportation.SetActive(leftActivate.action.ReadValue<Vector2>().y > 0.1f);


        if (!(rightCancel.action.ReadValue<Vector2>().x > 0.1f || rightCancel.action.ReadValue<Vector2>().x < -0.1f))
            rightTeleportation.SetActive(rightActivate.action.ReadValue<Vector2>().y > 0.1f);

    }
}
