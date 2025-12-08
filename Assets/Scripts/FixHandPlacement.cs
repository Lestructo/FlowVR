using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FixedHandPlacement : XRGrabInteractable
{
    public Transform leftHandAttachTransform;
    public Transform rightHandAttachTransform;

    public XRNode holdingHand = XRNode.LeftHand; // default, gets overwritten
    public bool isHeld = false;

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        // Get the interactor's controller root (parent of Near-Far Interactor)
        Transform controller = args.interactorObject.transform.parent;

        if (controller.CompareTag("LeftHand"))
        {
            attachTransform = leftHandAttachTransform;
            holdingHand = XRNode.LeftHand;     
            isHeld = true;                     
            Debug.Log("Left hand grabbing - attachTransform assigned");
        }
        else if (controller.CompareTag("RightHand"))
        {
            attachTransform = rightHandAttachTransform;
            holdingHand = XRNode.RightHand;    
            isHeld = true;                      
            Debug.Log("Right hand grabbing - attachTransform assigned");
        }
        else
        {
            Debug.LogWarning("Interactor did not match Left or Right hand tag");
        }

        base.OnSelectEntering(args);
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        isHeld = false; // <<< reset when released
        base.OnSelectExiting(args);
    }
}
