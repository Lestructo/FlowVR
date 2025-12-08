using UnityEngine;
using UnityEngine.XR;

public class BallHaptics : MonoBehaviour
{
    [Header("Racket Reference")]
    public FixedHandPlacement racketGrab;   // Drag your racket here

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
        // Only trigger when racket hits the ball
        if (!collision.collider.CompareTag("Racket"))
            return;

        // Racket not being held → don't send haptics
        if (racketGrab == null || !racketGrab.isHeld)
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

        // Always use the hand holding the racket
        XRNode node = racketGrab.holdingHand;

        InputDevice dev = InputDevices.GetDeviceAtXRNode(node);

        if (!dev.isValid)
        {
            Debug.LogWarning($"[HAPTICS] Device for {node} is NOT valid.");
            return;
        }

        bool sent = dev.SendHapticImpulse(0, amp, hapticDuration);

        Debug.Log(
            $"[HAPTICS] Racket collision → Hand={node}, Amp={amp:F2}, Vel={velocity:F2}, Success={sent}"
        );
    }
}
