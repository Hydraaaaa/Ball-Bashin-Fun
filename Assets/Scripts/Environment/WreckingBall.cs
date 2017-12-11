using UnityEngine;
using System.Collections;

public class WreckingBall : MonoBehaviour
{
    Rigidbody rb;
    public float maxSpeed;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        rb.velocity *= 1.001f;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
	}
}
