using UnityEngine;

public class PhysicsFollow : MonoBehaviour
{
    public Transform target;       // visual hand
    public Rigidbody rb;           // physics proxy
    public float positionStrength = 50f;
    public float rotationStrength = 50f;

    void FixedUpdate()
    {
        Vector3 posDelta = target.position - rb.position;
        Quaternion rotDelta = target.rotation * Quaternion.Inverse(rb.rotation);

        rb.linearVelocity = posDelta * positionStrength;
        rb.angularVelocity = rotDelta.eulerAngles * Mathf.Deg2Rad * rotationStrength;
    }
}
