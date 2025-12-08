using UnityEngine;

public class CollisionSounds : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip ballHitSound;
    public AudioClip racketHitSound;

    [Header("Volumes")]
    public float ballBaseVolume = 0.05f;
    public float ballMultiplier = 0.1f;

    public float racketBaseVolume = 0.2f;
    public float racketMultiplier = 0.2f;

    [Header("Physics Threshold")]
    public float minVelocity = 0.5f;

    [Header("Pitch Randomization")]
    public float pitchMin = 0.9f;
    public float pitchMax = 1.1f;

    [Header("Cooldown")]
    public float cooldown = 0.25f;

    private AudioSource ballSource;
    private AudioSource racketSource;

    // GLOBAL cooldown shared by all balls
    private static float globalLastSound = -10f;

    private void Start()
    {
        ballSource = gameObject.AddComponent<AudioSource>();
        racketSource = gameObject.AddComponent<AudioSource>();

        ballSource.spatialBlend = 1f;
        racketSource.spatialBlend = 1f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // GLOBAL cooldown
        if (Time.time - globalLastSound < cooldown)
            return;

        float v = collision.relativeVelocity.magnitude;
        if (v < minVelocity)
            return;

        bool hitRacket = collision.collider.CompareTag("Racket");

        // Randomize pitch separately
        float racketPitch = Random.Range(pitchMin, pitchMax);
        float ballPitch   = Random.Range(pitchMin, pitchMax);

        // -------- RACKET SOUND --------
        if (hitRacket)
        {
            float vol = Mathf.Clamp(racketBaseVolume + v * racketMultiplier, 0f, 1f);
            racketSource.pitch = racketPitch;
            racketSource.PlayOneShot(racketHitSound, vol);
        }

        // -------- BALL SOUND --------
        // Play ball bounce if:
        // - it's hitting racket OR
        // - it's hitting any non-ball collider
        if (hitRacket || !collision.collider.CompareTag("Ball"))
        {
            float vol = Mathf.Clamp(ballBaseVolume + v * ballMultiplier, 0f, 1f);
            ballSource.pitch = ballPitch;
            ballSource.PlayOneShot(ballHitSound, vol);
        }

        // Update GLOBAL cooldown
        globalLastSound = Time.time;
    }
}