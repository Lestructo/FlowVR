using UnityEngine;
using System.Collections;

public class BallBasketFiller : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform basketCenter;

    public float radius = 0.15f;       // half-width of the basket interior
    public float spawnHeight = 0.5f;   // spawning Y offset above basket center
    public float fillHeight = 0.6f;    // height at which basket is considered "full"
    public float spawnDelay = 0.2f;    // time between spawns

    public LayerMask ballMask;         // ONLY the Ball layer
    public LayerMask handMask;         // Ignore Raycast layer (your hands)

    private bool spawning = false;

    void Update()
    {
        // Only spawn if basket isn't full and no coroutine is running
        if (!IsBasketFull() && !spawning)
        {
            StartCoroutine(SpawnBall());
        }
    }

    IEnumerator SpawnBall()
    {
        spawning = true;

        Vector3 spawnPos = basketCenter.position + new Vector3(
            Random.Range(-radius, radius),
            spawnHeight,
            Random.Range(-radius, radius)
        );

        // SAFETY CHECK:
        // If a hand is inside the spawn zone → do NOT spawn
        if (Physics.CheckSphere(spawnPos, 0.1f, handMask))
        {
            spawning = false;
            yield break;
        }

        // Safe to spawn
        Instantiate(ballPrefab, spawnPos, Quaternion.identity);

        yield return new WaitForSeconds(spawnDelay);
        spawning = false;
    }

    bool IsBasketFull()
    {
        // Detects if ANY ball on the Ball layer is in the upper "full" zone
        return Physics.CheckBox(
            basketCenter.position + Vector3.up * fillHeight,
            new Vector3(radius, 0.1f, radius),
            Quaternion.identity,
            ballMask
        );
    }

    // Optional: draw gizmos to visualize basket detection zone
    void OnDrawGizmos()
    {
        if (basketCenter == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(
            basketCenter.position + Vector3.up * fillHeight,
            new Vector3(radius * 2, 0.2f, radius * 2)
        );
    }
}