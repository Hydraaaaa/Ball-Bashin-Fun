using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour
{
    Light light;

    public float intervalMin;
    public float intervalMax;
    float currentInterval;

    public float flickerDuration;
    float currentFlickerDuration;

    [Range(0, 100)]
    public float lightPercent;
    float originalIntensity;

    bool flickering;

    void Start ()
    {
        light = GetComponent<Light>();
        currentInterval = Random.Range(intervalMin, intervalMax);
        originalIntensity = light.intensity;
        flickering = false;
	}
	
	void Update ()
    {
        currentInterval -= Time.deltaTime;
        currentFlickerDuration -= Time.deltaTime;

        if (currentInterval <= 0 && !flickering)
        {
            light.intensity = light.intensity * (lightPercent / 100);
            currentFlickerDuration = flickerDuration;
            flickering = true;
        }

        if (currentFlickerDuration <= 0 && flickering)
        {
            light.intensity = originalIntensity;
            currentInterval = Random.Range(intervalMin, intervalMax);
            flickering = false;
        }
	}
}
