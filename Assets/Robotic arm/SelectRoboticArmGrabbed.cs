using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRoboticArm : GrabbedObjectScript
{
    [Header("Robotic Arm Object parameters")]
    [Tooltip("Is On or Off, only for Start")]
    public bool on_off;

    [Tooltip("The RoboticArm target to look at.")]
    public Transform target;
    [Tooltip("The Transform allowing to reset the target position. Set it to the Joint with the IK Manager.")]
    public Transform resetTarget;

    public float transformRate = 1f;
    private Vector3 _followOffSet;

    // Start is called before the first frame update
    void Start()
    {
        _followOffSet = target.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (on_off)
        {
            Vector3 targetPosition = transform.position + _followOffSet;

            // Keep our y position unchanged.
            //targetPosition.y = transform.position.y;

            // Smooth follow.    
            target.position += (targetPosition - target.position) * 0.1f *transformRate;
        }
    }

    public override void Activate()
    {
        base.Activate();
        on_off = true;
        _followOffSet = target.position - transform.position;
        Debug.Log("Is Activated");
    }

    public override void Deactivate()
    {
        base.Deactivate();
        on_off = false;
        // Reset the target to a value
        target.position = resetTarget.position;
        Debug.Log("Is Deactivated");
    }
}
