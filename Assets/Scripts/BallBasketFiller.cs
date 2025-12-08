using UnityEngine;
using System.Collections;

public class BallBasketFiller : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform basketCenter;

    [Header("Spawn Area")]
    public float radius = 0.1f;        // half-width of spawn area inside the basket
    public float spawnHeight = 0.3f;   // vertical height above basket center to spawn

    [Header("Fill Detection")]
    public float fillHeight = 0.3f;    // vertical height at which basket is considered "full"
    public float fillThickness = 0.1f; // thickness of the fill detection box

    [Header("Spawn Timing")]
    public float spawnDelay = 0.1f;

    private bool spawning = false;

    void Update()
    {
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

        Instantiate(ballPrefab, spawnPos, Quaternion.identity);

        yield return new WaitForSeconds(spawnDelay);
        spawning = false;
    }

    bool IsBasketFull()
    {
        // Checks if *any object with a collider* is in the upper fill zone
        return Physics.CheckBox(
            basketCenter.position + Vector3.up * fillHeight,
            new Vector3(radius, fillThickness, radius),
            Quaternion.identity
        );
    }

    void OnDrawGizmos()
    {
        if (basketCenter == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(
            basketCenter.position + Vector3.up * fillHeight,
            new Vector3(radius * 2, fillThickness * 2, radius * 2)
        );
    }
}