using UnityEngine;
using System.Collections;

public class Rain : MonoBehaviour
{
    public float timeLeft;
    float time = 0;
    public Material objColour;
    public GameObject lightObj;
    float delayTime;
    public float startDelay;
    public float redC;
    public float blueC;
    public float greenC;
    public float alphaC;
    public float lightIntensity;
    public float FadeIntensity;
    public float rainSize;

    public bool LightOn;
    public bool FadeOn;
    public bool spawnOn;

    //x - 21/2, -55/2 10.5, -27.5
    //z - 38/2, -34/2 19, -17

    public float yAxis;
    float maggots;
    float in_a; //y axis
    float locker;


    Raindrop[] rainArray;
    float[] timeArray;

    public int maxRain;
    public int raindrops;
    int count = 0;

    public AudioClip rainStartSound;

    // Use this for initialization
    void Start()
    {
        //Camera.main.GetComponent<AudioSource>().clip = rainStartSound;
        //Camera.main.GetComponent<AudioSource>().Play();
        redC /= 255;
        blueC /= 255;
        greenC /= 255;
        alphaC /= 255;
        FadeIntensity /= 255;
        if (!FadeOn && LightOn)
        {
            lightObj.GetComponent<Light>().color = new Vector4(redC, greenC, blueC, alphaC);
        }
        lightObj.GetComponent<Light>().intensity = lightIntensity;
        rainArray = new Raindrop[maxRain];
        timeArray = new float[maxRain];
        in_a = yAxis;
        Camera.main.GetComponent<AudioSource>().clip = rainStartSound;
        Camera.main.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        #region Fading
        if (FadeOn && LightOn)
        {
            if (lightObj.GetComponent<Light>().color.a > alphaC)
            {
                lightObj.GetComponent<Light>().color = new Vector4(lightObj.GetComponent<Light>().color.r, lightObj.GetComponent<Light>().color.g, lightObj.GetComponent<Light>().color.b, lightObj.GetComponent<Light>().color.a - FadeIntensity);
            }
            if (lightObj.GetComponent<Light>().color.a < alphaC)
            {
                lightObj.GetComponent<Light>().color = new Vector4(lightObj.GetComponent<Light>().color.r, lightObj.GetComponent<Light>().color.g, lightObj.GetComponent<Light>().color.b, lightObj.GetComponent<Light>().color.a + FadeIntensity);
            }

            if (lightObj.GetComponent<Light>().color.r > redC)
            {
                lightObj.GetComponent<Light>().color = new Vector4(lightObj.GetComponent<Light>().color.r - FadeIntensity, lightObj.GetComponent<Light>().color.g, lightObj.GetComponent<Light>().color.b, lightObj.GetComponent<Light>().color.a);
            }
            if (lightObj.GetComponent<Light>().color.r < redC)
            {
                lightObj.GetComponent<Light>().color = new Vector4(lightObj.GetComponent<Light>().color.r + FadeIntensity, lightObj.GetComponent<Light>().color.g, lightObj.GetComponent<Light>().color.b, lightObj.GetComponent<Light>().color.a);
            }

            if (lightObj.GetComponent<Light>().color.g > greenC)
            {
                lightObj.GetComponent<Light>().color = new Vector4(lightObj.GetComponent<Light>().color.r, lightObj.GetComponent<Light>().color.g - FadeIntensity, lightObj.GetComponent<Light>().color.b, lightObj.GetComponent<Light>().color.a);
            }
            if (lightObj.GetComponent<Light>().color.g < greenC)
            {
                lightObj.GetComponent<Light>().color = new Vector4(lightObj.GetComponent<Light>().color.r, lightObj.GetComponent<Light>().color.g + FadeIntensity, lightObj.GetComponent<Light>().color.b, lightObj.GetComponent<Light>().color.a);
            }

            if (lightObj.GetComponent<Light>().color.b > blueC)
            {
                lightObj.GetComponent<Light>().color = new Vector4(lightObj.GetComponent<Light>().color.r, lightObj.GetComponent<Light>().color.g, lightObj.GetComponent<Light>().color.b - FadeIntensity, lightObj.GetComponent<Light>().color.a);
            }
            if (lightObj.GetComponent<Light>().color.b < blueC)
            {
                lightObj.GetComponent<Light>().color = new Vector4(lightObj.GetComponent<Light>().color.r, lightObj.GetComponent<Light>().color.g, lightObj.GetComponent<Light>().color.b + FadeIntensity, lightObj.GetComponent<Light>().color.a);
            }
        }
        #endregion
        if (delayTime < startDelay)
        {
            delayTime += Time.deltaTime;
            return;
        }
        if (raindrops < 0)
        {
            raindrops = 0;
        }
        if (spawnOn)
        {
            time += Time.deltaTime;
            if (time >= timeLeft && raindrops < maxRain)
            {
                time = 0;
                if (timeArray[count] <= 0)
                {
                    rainArray[count] = new Raindrop(rainSize);
                    timeArray[count] = 5.0f;
                    maggots = Random.Range(-50, 50);
                    while (maggots >= -27.5 && maggots <= 10.5)
                        maggots = Random.Range(-50, 50);

                    locker = Random.Range(-50, 50);
                    while (locker >= -17 && locker <= 19)
                        locker = Random.Range(-50, 50);

                    rainArray[count].SetLocation(new Vector3(maggots, in_a, locker));
                    rainArray[count].SetRainDropColour(objColour);
                    raindrops++;
                    count++;
                    if (count == maxRain)
                    {
                        count = 0;
                    }
                }
            }
        }
        for (int i = 0; i < maxRain; i++)
        {
            if (timeArray[i] > 0)
            {
                timeArray[i] -= Time.deltaTime;
            }
            else
            {
                //if (rainArray[i].gameObject != null)
                {
                    //if (rainArray[i] != null)
                    {
                        rainArray[i].dropDestroy();
                        raindrops--;
                    }
                }
            }
        }
        //for (int i = 0; i < maxRain; i++)
        //{
        //    if (rainArray[i] !=  null)
        //    {
        //        rainArray[i].rainFnct();
        //        //if (rainArray[i] == null)
        //        //{
        //        //    raindrops--;
        //        //}
        //    }
        //    if (rainArray[i].gameObject == null)
        //    {
        //        raindrops--;
        //    }
        //}
    }

    public void SetSpawnOn(bool a_Set)
    {
        spawnOn = a_Set;
    }
}
