using UnityEngine;
using UnityEngine.XR;

public class BallHaptics : MonoBehaviour
{
    [Header("Hand Transforms")]
    public Transform leftHand;
    public Transform rightHand;

    [Header("Haptics Settings")]
    public float minVelocity = 0.5f;
    public float minHaptic = 0.1f;
    public float maxHaptic = 0.5f;
    public float hapticDuration = 0.1f;

    [Header("Cooldown")]
    public float hapticCooldown = 0.25f;

    private static float globalLastHaptic = -10f;

    private void OnCollisionEnter(Collision collision)
    {
        // Only trigger when racket hits ball
        if (!collision.collider.CompareTag("Racket"))
            return;

        // Global cooldown
        if (Time.time - globalLastHaptic < hapticCooldown)
            return;

        float v = collision.relativeVelocity.magnitude;
        if (v < minVelocity)
            return;

        TriggerHaptic(v);
        globalLastHaptic = Time.time;
    }

    private void TriggerHaptic(float velocity)
    {
        float amp = Mathf.Clamp(velocity / 8f, minHaptic, maxHaptic);

        float leftDist = Vector3.Distance(transform.position, leftHand.position);
        float rightDist = Vector3.Distance(transform.position, rightHand.position);

        // Choose closest hand
        XRNode node = (leftDist < rightDist) ? XRNode.LeftHand : XRNode.RightHand;

        Debug.Log($"[HAPTICS] Collision detected → LeftDist={leftDist:F3}, RightDist={rightDist:F3}, Closest={node}");

        InputDevice dev = InputDevices.GetDeviceAtXRNode(node);

        if (!dev.isValid)
        {
            Debug.LogWarning($"[HAPTICS] Device for {node} is NOT valid. (Editor mode or no XR headset connected)");
            return;
        }

        bool sent = dev.SendHapticImpulse(0, amp, hapticDuration);

        Debug.Log(
            $"[HAPTICS] SENT → Hand={node}, Amp={amp:F2}, Duration={hapticDuration:F2}, Velocity={velocity:F2}, Success={sent}"
        );
    }
}
