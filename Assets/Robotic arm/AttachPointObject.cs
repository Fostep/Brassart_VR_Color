using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPointObject : MonoBehaviour
{
    public GameObject attachPointObject;

    private bool holdActivated;
    // Start is called before the first frame update
    void Start()
    {
        holdActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (holdActivated && attachPointObject != null)
        {
            attachPointObject.transform.position = transform.position;
            attachPointObject.transform.rotation = transform.rotation;
        }
    }

    public void Activate()
    {
        if (holdActivated)
        {

        }
        
    }
}
