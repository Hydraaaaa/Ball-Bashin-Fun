using UnityEngine;
using System.Collections;

public class FlowerWind : MonoBehaviour
{
    float timer;
    public float minInterval;
    public float maxInterval;
    public GameObject windObject;

	void Start ()
    {
        timer = Random.Range(minInterval, maxInterval);
	}
	
	void Update ()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(0, 1f)).normalized * 140 + new Vector3(0, -10, 0);
            Instantiate(windObject, spawnPos, Quaternion.identity);
            
            timer = Random.Range(minInterval, maxInterval);
        }
	}
}
