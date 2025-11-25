using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BallSounds : MonoBehaviour
{
    public AudioClip hitSound;

    [Header("Volumes")]
    public float defaultVolume = 0.5f;
    public float racketVolume = 0.1f;

    [Header("Physics Threshold")]
    public float minVelocityForSound = 0.5f;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Skip collisions with other balls
        if (collision.collider.CompareTag("Ball"))
            return;

        // total collision speed based on relative rigidbody motion
        float impactVelocity = collision.relativeVelocity.magnitude;

        // Skip tiny bumps
        if (impactVelocity < minVelocityForSound)
            return;

        // Pick volume
        float volume = collision.collider.CompareTag("Racket")
            ? racketVolume
            : defaultVolume;

        audioSource.PlayOneShot(hitSound, volume);
    }
}