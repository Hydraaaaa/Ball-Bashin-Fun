using UnityEngine;
using System.Collections;

public class TimedLife : MonoBehaviour
{
    public float Life;
    float currentLife;

	void Start ()
    {
        currentLife = Life;
	}
	
	void Update ()
    {
        currentLife -= Time.deltaTime;
        if (currentLife <= 0)
            Destroy(gameObject);
	}
}
