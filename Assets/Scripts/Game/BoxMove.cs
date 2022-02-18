using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class BoxMove : MonoBehaviour
{
    [Header("RigidBody Reference")]
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Push(Vector3 direction, float power)
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(direction * power, ForceMode.Impulse);
    }   
}
