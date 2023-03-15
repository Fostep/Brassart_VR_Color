using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabbedObjectScript : XRGrabInteractable
{

    private GameObject _currentHand;

    // The object is grabbed
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);
        if(interactor.GetComponent<Hand>() != null)
        {
            _currentHand = interactor.gameObject;
            interactor.GetComponent<Hand>().SetGrabbedObject(this.gameObject);
        }

        //Debug.Log("Grabbed object: " + gameObject.name);
        //Debug.Log("Interactor object: " + interactor.name);
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        Debug.Log("OnSelectEntering: " + args.interactorObject.transform.name);
    }

    // The object isn't grabbed anymore
    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);
        interactor.GetComponent<Hand>().SetGrabbedObject(null);
        _currentHand = null;

        //Debug.Log("Let object go: "+ gameObject.name);
        //Debug.Log("hand letting it go: "+interactor.name);
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);
        Debug.Log("OnSelectExiting: " + args);
    }

    protected override void OnActivate(XRBaseInteractor interactor)
    {
        base.OnActivate(interactor);
        Activate();
    }

    protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args);
        Debug.Log("OnActivated: " + args);
    }

    protected override void OnDeactivate(XRBaseInteractor interactor)
    {
        base.OnDeactivate(interactor);
        //Debug.Log(this.name + "IS DEACTIVATED");
    }

    protected override void OnDeactivated(DeactivateEventArgs args)
    {
        base.OnDeactivated(args);
        Debug.Log("OnDeactivated: "+args);
    }

    public virtual void Activate()
    {
        Debug.Log(this.name + " is activated");
    }
}
