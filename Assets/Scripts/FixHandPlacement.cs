using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FixedHandPlacement : XRGrabInteractable
{
    public Transform leftHandAttachTransform;
    public Transform rightHandAttachTransform;

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        // Get the interactor's controller root (parent of Near-Far Interactor)
        Transform controller = args.interactorObject.transform.parent;

        if (controller.CompareTag("LeftHand"))
        {
            attachTransform = leftHandAttachTransform;
            Debug.Log("Left hand grabbing - attachTransform assigned");
        }
        else if (controller.CompareTag("RightHand"))
        {
            attachTransform = rightHandAttachTransform;
            Debug.Log("Right hand grabbing - attachTransform assigned");
        }
        else
        {
            Debug.LogWarning("Interactor did not match Left or Right hand tag");
        }

        base.OnSelectEntering(args);
    }
}