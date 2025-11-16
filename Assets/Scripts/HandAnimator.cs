using UnityEngine;
using UnityEngine.XR;

public class HandAnimatorController : MonoBehaviour
{
    public XRNode handNode; // LeftHand or RightHand
    private InputDevice device;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        device = InputDevices.GetDeviceAtXRNode(handNode);
    }

    void Update()
    {
        if (!device.isValid)
            device = InputDevices.GetDeviceAtXRNode(handNode);

        if (device.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            animator.SetFloat("Grip", gripValue);

        if (device.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
            animator.SetFloat("Trigger", triggerValue);
    }
}
