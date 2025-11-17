using UnityEngine;
using System.Collections;

public class BallBasketFiller : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform basketCenter;
    public float radius = 0.15f; // adjust to basket width
    public float spawnHeight = 0.5f; // above center
    public int maxBalls = 50;
    public float fillHeight = 0.6f; // Y height where "full" is

    private int ballCount = 0;

    private void Start()
    {
        StartCoroutine(SpawnBalls());
    }

    IEnumerator SpawnBalls()
    {
        while (ballCount < maxBalls)
        {
            // Check if basket is full
            if (IsBasketFull())
                yield break;

            Vector3 spawnPos = basketCenter.position + new Vector3(
                Random.Range(-radius, radius),
                spawnHeight,
                Random.Range(-radius, radius)
            );

            Instantiate(ballPrefab, spawnPos, Quaternion.identity);
            ballCount++;

            // Give time for physics to settle
            yield return new WaitForSeconds(0.2f);
        }
    }

    bool IsBasketFull()
    {
        return Physics.CheckBox(
            basketCenter.position + Vector3.up * fillHeight,
            new Vector3(radius, 0.1f, radius),
            Quaternion.identity,
            LayerMask.GetMask("Ball")
        );
    }
}
