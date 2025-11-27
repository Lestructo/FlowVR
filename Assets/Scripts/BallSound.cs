using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BallSounds : MonoBehaviour
{
    public AudioClip hitSound;

    [Header("Volumes")]
    public float baseVolume = 0.1f;      
    public float volumeMultiplier = 0.25f;

    [Header("Physics Threshold")]
    public float minVelocityForSound = 0.5f;

    [Header("Pitch Randomization")]
    public float pitchMin = 0.95f;
    public float pitchMax = 1.05f;

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
        // Ignore other balls
        if (collision.collider.CompareTag("Ball"))
            return;

        // Cooldown
        if (Time.time - lastSound < cooldown)
            return;

        float v = collision.relativeVelocity.magnitude;
        if (v < minVelocityForSound)
            return;

        // Normal "environment collision" volume
        float volume = Mathf.Clamp(baseVolume + v * volumeMultiplier, 0f, 1f);

        // Random pitch variation
        audioSource.pitch = Random.Range(pitchMin, pitchMax);

        audioSource.PlayOneShot(hitSound, volume);
        lastSound = Time.time;
    }
}
