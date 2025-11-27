using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(AudioSource))]
public class RacketHitSound : MonoBehaviour
{
    public AudioClip racketHitSound;

    [Header("Volume Scaling")]
    public float baseVolume = 0.1f;
    public float volumeMultiplier = 0.25f;   // louder when faster

    [Header("Pitch Randomization")]
    public float pitchMin = 0.95f;
    public float pitchMax = 1.05f;

    [Header("Haptics")]
    public bool vibrateLeft = true;
    public bool vibrateRight = true;
    public float minHaptic = 0.1f;     // weakest rumble
    public float maxHaptic = 0.5f;     // strongest rumble
    public float hapticDuration = 0.1f;

    [Header("Cooldown")]
    public float cooldown = 0.1f;

    private AudioSource audioSource;
    private float lastSound = -10f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Ball"))
            return;

        if (Time.time - lastSound < cooldown)
            return;

        // Impact speed
        float v = collision.relativeVelocity.magnitude;

        // AUDIO VOLUME BASED ON SPEED
        float volume = Mathf.Clamp(baseVolume + v * volumeMultiplier, 0f, 1f);

        // RANDOM PITCH
        audioSource.pitch = Random.Range(pitchMin, pitchMax);
        audioSource.PlayOneShot(racketHitSound, volume);

        // HAPTICS BASED ON SPEED
        float amp = Mathf.Clamp(v / 8f, minHaptic, maxHaptic);

        if (vibrateLeft)  SendHaptics(XRNode.LeftHand, amp, hapticDuration);
        if (vibrateRight) SendHaptics(XRNode.RightHand, amp, hapticDuration);

        lastSound = Time.time;
    }

    void SendHaptics(XRNode hand, float amplitude, float duration)
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(hand);
        if (device.isValid)
        {
            device.SendHapticImpulse(0, amplitude, duration);
        }
    }
}
