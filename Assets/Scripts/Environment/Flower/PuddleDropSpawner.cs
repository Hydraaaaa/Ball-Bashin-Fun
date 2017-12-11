using UnityEngine;
using System.Collections;

public class PuddleDropSpawner : MonoBehaviour
{
    public GameObject rain;

    public float intervalMin;
    public float intervalMax;
    float currentInterval;

    public float spawnRadius;
    public float spawnHeight;

    void Start ()
    {
        currentInterval = Random.Range(intervalMin, intervalMax);
	}
	
	void Update ()
    {
        currentInterval -= Time.deltaTime;

        if (currentInterval <= 0)
        {
            currentInterval = Random.Range(intervalMin, intervalMax);

            Vector2 spawnPosition;
            spawnPosition = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            if (spawnPosition.magnitude > 1)
                spawnPosition.Normalize();

            spawnPosition *= spawnRadius;

            Instantiate(rain, new Vector3(spawnPosition.x, spawnHeight, spawnPosition.y), Quaternion.identity);
        }

	}
}
