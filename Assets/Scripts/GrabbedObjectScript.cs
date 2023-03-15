using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabbedObjectScript : XRGrabInteractable
{

    private GameObject _currentHand;
    private bool _isActive;

    public bool GetIsActive()
    {
        return _isActive;
    }

    public void SetIsActive(bool bo)
    {
        _isActive = bo;
    }

    // The object is grabbed
    /*
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
    */

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);

        if(args.interactorObject.transform.GetComponent<Hand>() != null)
        {
            _currentHand = args.interactorObject.transform.gameObject;
            _currentHand.GetComponent<Hand>().SetGrabbedObject(this.gameObject);
        }

        //Debug.Log("OnSelectEntering: " + args.interactorObject.transform.name);
    }

    // The object isn't grabbed anymore
    /*
    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);
        interactor.GetComponent<Hand>().SetGrabbedObject(null);
        _currentHand = null;

        //Debug.Log("Let object go: "+ gameObject.name);
        //Debug.Log("hand letting it go: "+interactor.name);
    }
    */

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);
        if(_currentHand != null)
        {
            _currentHand.GetComponent<Hand>().SetGrabbedObject(null);
            _currentHand = null;
        }

        //Debug.Log("OnSelectExiting: " + args);
    }

    /*
    protected override void OnActivate(XRBaseInteractor interactor)
    {
        base.OnActivate(interactor);
        Activate();
    }
    */

    protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args);

        Activate();
        //Debug.Log("OnActivated: " + args);
    }

    /*
    protected override void OnDeactivate(XRBaseInteractor interactor)
    {
        base.OnDeactivate(interactor);
        //Debug.Log(this.name + "IS DEACTIVATED");
    }
    */

    protected override void OnDeactivated(DeactivateEventArgs args)
    {
        base.OnDeactivated(args);

        Deactivate();
        //Debug.Log("OnDeactivated: "+args);
    }

    public virtual void Activate()
    {
        
        //Debug.Log(this.name + " Object is activated");
    }

    public virtual void Deactivate()
    {
        
        //Debug.Log(this.name + " Object is deactivated");
    }
}
