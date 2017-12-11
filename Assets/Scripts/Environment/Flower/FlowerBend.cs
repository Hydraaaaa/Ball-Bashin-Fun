using UnityEngine;
using System.Collections;

public class FlowerBend : MonoBehaviour
{
    float duration;
    Vector3 direction;
    public float rotationLimit;
    public float rotationSpeed;
    float currentRotation;

	void Start ()
    {
        duration = 0;
        currentRotation = 0;
	}
	
	void Update ()
    {
        duration -= Time.deltaTime;

        if (duration > 0 && currentRotation < rotationLimit)
        {
            transform.Rotate(direction, Time.deltaTime * rotationSpeed);
            currentRotation += Time.deltaTime * rotationSpeed;
        }

        if (duration <= 0 && currentRotation > 0)
        {
            transform.Rotate(-direction, Time.deltaTime * rotationSpeed);
            currentRotation -= Time.deltaTime * rotationSpeed;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WindTrigger"))
        {
            direction = other.transform.right;
            duration = other.GetComponent<WindObjectAI>().duration;
        }
    }
}
