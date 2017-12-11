using UnityEngine;
using System.Collections;

public class PuddleDrop : MonoBehaviour
{
    public GameObject puddle;

    public float yOffset;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Flower"))
        {
            Instantiate(puddle, transform.position + new Vector3(0, yOffset, 0), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
