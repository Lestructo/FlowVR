using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketDisruption : MonoBehaviour
{
    [Header("Settings")]
    public GameObject allBallsParent; // Drag the "All_Balls" GameObject here

    [Header("Timing")]
    public float gracePeriod = 60f; // Time AFTER first hit before disruption starts
    public float disableDuration = 15f; // How long collisions stay disabled

    [Header("Debug")]
    public bool debugLogs = true;

    private List<Collider> racketColliders = new List<Collider>();
    private bool hasDisrupted = false;

    void Start()
    {
        // Get ALL colliders on the racket (including deep children)
        FindAllRacketColliders();

        if (debugLogs)
        {
            Debug.Log($"<color=cyan>RacketDisruption: Found {racketColliders.Count} colliders</color>");
            foreach (Collider col in racketColliders)
            {
                string type = col.GetType().Name;
                Debug.Log($"  - {col.name} ({type}, trigger: {col.isTrigger})");
            }
        }
    }

    void FindAllRacketColliders()
    {
        // Clear the list
        racketColliders.Clear();

        // Get all colliders on this object and all children
        Collider[] allColliders = GetComponentsInChildren<Collider>(true);

        foreach (Collider col in allColliders)
        {
            // Skip trigger colliders (they don't fire OnCollisionEnter)
            if (col.isTrigger)
            {
                if (debugLogs) Debug.Log($"Skipping trigger collider: {col.name}");
                continue;
            }

            racketColliders.Add(col);
        }

        if (racketColliders.Count == 0)
        {
            Debug.LogError($"<color=red>No non-trigger colliders found on racket! Check if colliders have 'Is Trigger' unchecked.</color>");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (debugLogs)
            Debug.Log($"<color=yellow>Collision: {name} hit {collision.gameObject.name} via {collision.collider.name}</color>");

        if (hasDisrupted)
        {
            if (debugLogs) Debug.Log("Already disrupted - skipping");
            return;
        }

        if (IsBallCollision(collision.gameObject))
        {
            if (debugLogs) Debug.Log($"<color=green>FIRST BALL HIT! Starting disruption...</color>");
            hasDisrupted = true;
            StartCoroutine(DisruptionRoutine());
        }
    }

    bool IsBallCollision(GameObject hitObject)
    {
        if (allBallsParent == null)
        {
            Debug.LogError("All_Balls parent not assigned!");
            return false;
        }

        // Check if hitObject is a child of All_Balls
        if (hitObject.transform.IsChildOf(allBallsParent.transform))
        {
            if (debugLogs) Debug.Log($"{hitObject.name} is child of {allBallsParent.name}");
            return true;
        }

        // Alternative: check parents
        Transform parent = hitObject.transform.parent;
        while (parent != null)
        {
            if (parent.gameObject == allBallsParent)
            {
                if (debugLogs) Debug.Log($"{hitObject.name} is under {allBallsParent.name} via parent {parent.name}");
                return true;
            }
            parent = parent.parent;
        }

        if (debugLogs) Debug.Log($"{hitObject.name} is NOT a ball");
        return false;
    }

    IEnumerator DisruptionRoutine()
    {
        if (debugLogs) Debug.Log($"<color=orange>=== DISRUPTION STARTED ===</color>");
        if (debugLogs) Debug.Log($"Grace period: {gracePeriod}s");

        // GRACE PERIOD - collisions still work
        yield return new WaitForSeconds(gracePeriod);

        // GET ALL BALL COLLIDERS
        List<Collider> allBallColliders = GetAllBallColliders();

        if (allBallColliders.Count == 0)
        {
            Debug.LogError("No ball colliders found!");
            yield break;
        }

        if (debugLogs)
        {
            Debug.Log($"Racket colliders: {racketColliders.Count}");
            Debug.Log($"Ball colliders: {allBallColliders.Count}");
            Debug.Log($"Disabling collisions for {disableDuration}s...");
        }

        // DISABLE COLLISIONS BETWEEN ALL RACKET AND ALL BALL COLLIDERS
        DisableCollisions(racketColliders, allBallColliders, true);

        // WAIT
        yield return new WaitForSeconds(disableDuration);

        // RE-ENABLE COLLISIONS
        DisableCollisions(racketColliders, allBallColliders, false);

        if (debugLogs) Debug.Log($"<color=orange>=== DISRUPTION ENDED ===</color>");
    }

    List<Collider> GetAllBallColliders()
    {
        List<Collider> ballColliders = new List<Collider>();

        if (allBallsParent == null)
        {
            Debug.LogError("All_Balls parent not assigned!");
            return ballColliders;
        }

        // Get all direct children of All_Balls
        foreach (Transform ballTransform in allBallsParent.transform)
        {
            // Get ALL colliders from this ball and its children
            Collider[] colliders = ballTransform.GetComponentsInChildren<Collider>(true);
            ballColliders.AddRange(colliders);

            if (debugLogs)
                Debug.Log($"Ball '{ballTransform.name}': {colliders.Length} colliders");
        }

        return ballColliders;
    }

    void DisableCollisions(List<Collider> sourceColliders, List<Collider> targetColliders, bool ignore)
    {
        int count = 0;

        foreach (Collider racketCol in sourceColliders)
        {
            if (racketCol == null) continue;

            foreach (Collider ballCol in targetColliders)
            {
                if (ballCol == null) continue;

                // Skip if they're the same collider (shouldn't happen)
                if (racketCol == ballCol) continue;

                // Apply ignore collision
                Physics.IgnoreCollision(racketCol, ballCol, ignore);
                count++;
            }
        }

        if (debugLogs)
        {
            string action = ignore ? "Disabled" : "Enabled";
            Debug.Log($"{action} {count} collision pairs");
        }
    }

    // Visual debug in Scene view
    void OnDrawGizmosSelected()
    {
        if (racketColliders.Count > 0)
        {
            Gizmos.color = hasDisrupted ? Color.red : Color.green;
            foreach (Collider col in racketColliders)
            {
                if (col == null) continue;

                // Draw wireframe around each collider
                if (col is BoxCollider box)
                {
                    Gizmos.matrix = col.transform.localToWorldMatrix;
                    Gizmos.DrawWireCube(box.center, box.size);
                    Gizmos.matrix = Matrix4x4.identity;
                }
                else if (col is SphereCollider sphere)
                {
                    Gizmos.DrawWireSphere(col.transform.TransformPoint(sphere.center), sphere.radius);
                }
                else if (col is CapsuleCollider capsule)
                {
                    // Simplified capsule visualization
                    Vector3 center = col.transform.TransformPoint(capsule.center);
                    float height = capsule.height;
                    float radius = capsule.radius;
                    Gizmos.DrawWireSphere(center, radius);
                }
                else if (col is MeshCollider meshCol)
                {
                    if (meshCol.sharedMesh != null)
                    {
                        Gizmos.DrawWireMesh(meshCol.sharedMesh, col.transform.position, col.transform.rotation, col.transform.lossyScale);
                    }
                }
            }
        }
    }

    // Manual trigger for testing
    [ContextMenu("Test Disruption")]
    public void TestDisruption()
    {
        if (!hasDisrupted)
        {
            hasDisrupted = true;
            StartCoroutine(DisruptionRoutine());
        }
    }

    [ContextMenu("Reset Disruption")]
    public void ResetDisruption()
    {
        hasDisrupted = false;
        StopAllCoroutines();

        // Re-enable any disabled collisions
        List<Collider> ballColliders = GetAllBallColliders();
        DisableCollisions(racketColliders, ballColliders, false);

        Debug.Log("Disruption reset");
    }
}