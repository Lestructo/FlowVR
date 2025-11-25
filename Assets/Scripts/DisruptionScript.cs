using UnityEngine;
using System.Collections;

public class DisruptionScript : MonoBehaviour
{
    public GameObject racketRoot;
    private Collider ballCollider;
    private Collider[] racketColliders;
    private bool timerStarted = false;

    private void Start()
    {
        ballCollider = GetComponent<Collider>();
        racketColliders = racketRoot.GetComponentsInChildren<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == racketRoot && !timerStarted)
        {
            timerStarted = true;
            StartCoroutine(TimerRoutine());
        }
    }

    IEnumerator TimerRoutine()
    {
        yield return new WaitForSeconds(45f);

        foreach (var col in racketColliders)
            Physics.IgnoreCollision(ballCollider, col, true);

        yield return new WaitForSeconds(15f);

        foreach (var col in racketColliders)
            Physics.IgnoreCollision(ballCollider, col, false);
    }
}
