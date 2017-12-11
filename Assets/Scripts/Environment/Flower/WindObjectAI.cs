using UnityEngine;
using System.Collections;

public class WindObjectAI : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    public float speed;

    public float minDuration;
    public float maxDuration;
    public float duration;


    void Awake()
    {
        transform.LookAt(new Vector3(0, transform.position.y, 0));
        speed = Random.Range(minSpeed, maxSpeed);
        duration = Random.Range(minDuration, maxDuration);
    }
	
	void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        if (transform.position.magnitude > 200)
            Destroy(gameObject);
	}
}
