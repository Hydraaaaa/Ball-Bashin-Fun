using UnityEngine;
using System.Collections;

public class PlatformDrop : MonoBehaviour
{
    public GameObject cam;
    public float intensity;
    public float shakeWidth;
    bool shakeLeft;
    float timeShaking;
    float originalPosition;
    Vector3 originalLocation;
    public float ShakeTime;
    bool startShake = true;

    public AudioClip boulderHitSound;
    public AudioClip collapseWarningSound;

    public GameObject[] platforms;
    GameObject dropPlatform;
    public Vector3[] platformLocations;
    Vector3 dropLocation;
    int platNum;

    public bool[] hasBeenSpawned;

    public Material matObj;
    public Mesh matMesh;


    public float timeBetweenDropMin;
    public float timeBetweenDropMax;
    float timeBetweenDrop;
    float timeTaken;

    public float weight;
    public float boulderSize;
    GameObject boulder;
    bool noCall = false;
    public bool makeKinematic;

    bool startdrop;
    bool currentDrop;
    bool currentDropStart;
    public float start_delay;
    float startdelay;
    AudioSource audio;
    // public float platformMass;

    //These variables enable destroying the dropped platform when it reaches too low
    public bool destroyPlatforms;
    public float deathHeight;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        originalPosition = cam.transform.position.z;
        originalLocation = cam.transform.position;
        if (deathHeight > 0)
            deathHeight = -deathHeight;

        timeBetweenDrop = Random.Range(timeBetweenDropMin, timeBetweenDropMax);
    }
    void Update()
    {
        if (startShake)
        {
            audio.clip = collapseWarningSound;
            if (!audio.isPlaying)
                audio.Play();

            if (shakeLeft)
            {
                cam.transform.position = new Vector3(cam.transform.position.x + intensity, cam.transform.position.y, cam.transform.position.z + intensity);
            }
            else
            {
                cam.transform.position = new Vector3(cam.transform.position.x - intensity, cam.transform.position.y, cam.transform.position.z - intensity);
            }
            if (cam.transform.position.z >= originalPosition + shakeWidth)
            {
                shakeLeft = false;
            }
            if (cam.transform.position.z <= originalPosition - shakeWidth)
            {
                shakeLeft = true;
            }
            timeShaking += Time.deltaTime;
            if (timeShaking >= ShakeTime)
            {
                startShake = false;
                cam.transform.position = originalLocation;
            }
        }
        if (!startdrop && !currentDrop && !noCall && !startShake)
        {
            startdelay += Time.deltaTime;
            if (startdelay >= timeBetweenDrop)
            {
                //startdrop = true;
                timeBetweenDrop = Random.Range(timeBetweenDropMin, timeBetweenDropMax);
                platNum = Random.Range(0, platforms.Length);
                if (!hasBeenSpawned[platNum])
                {
                    dropPlatform = platforms[platNum];
                    dropLocation = platformLocations[platNum];
                    hasBeenSpawned[platNum] = true;
                }
                //if (dropPlatform == null)
                //{
                //    noCall = true;
                //    for (int i = 0; i < platforms.Length; i++)
                //    {
                //        if (platforms[i] != null)
                //        {
                //            dropPlatform = platforms[i];
                //            noCall = false;
                //        }
                //    } 
                //}
                else if (hasBeenSpawned[platNum])
                {
                    noCall = true;
                    for (int i = 0; i < hasBeenSpawned.Length; i++)
                    {
                        if (!hasBeenSpawned[i])
                        {
                            dropPlatform = platforms[i];
                            dropLocation = platformLocations[i];
                            hasBeenSpawned[i] = true;
                            noCall = false;
                            break;
                        }
                    }
                }
                currentDrop = true;
                currentDropStart = true;
                startdelay = 0;
            }
        }
        if (!startdrop && currentDrop && !noCall)
        {
            if (currentDropStart)
            {
                boulder = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                boulder.transform.position = new Vector3(dropLocation.x, 30, dropLocation.z);
                boulder.GetComponent<Renderer>().material = matObj;
                boulder.transform.localScale = new Vector3(boulderSize, boulderSize, boulderSize);
                boulder.GetComponent<MeshFilter>().mesh = matMesh;
                boulder.AddComponent<Rigidbody>();
                boulder.GetComponent<Rigidbody>().velocity = new Vector3(0, -weight, 0);
                currentDropStart = false;
            }
            boulder.transform.position = new Vector3(boulder.transform.position.x, boulder.transform.position.y - Time.deltaTime, boulder.transform.position.z);
            //if (boulder.transform.position.z != dropLocation.z)
            //{
            //    boulder.transform.position = new Vector3(boulder.transform.position.x, boulder.transform.position.y, dropLocation.z);
            //    Destroy(boulder);
            //    currentDrop = false;
            //    startdrop = false;
            //}
            if (boulder.transform.position.y <= dropLocation.y + 0.5f)
            {
                Destroy(boulder);
                audio.clip = boulderHitSound;
                audio.Play();
                currentDrop = false;
                startdrop = true;
            }
        }

        if (startdrop && !noCall)
        {
            //timeTaken += Time.deltaTime;
            //    if (timeTaken >= timeBetweenDrop)
            //    {
            //        //dropPlatform = platforms[Random.Range(0, platforms.Length)];
            //        if (dropPlatform.GetComponent<Rigidbody>() != null)
            //        {
            //            for (int i = 0; i <= platforms.Length; i++)
            //            {
            //                if (platforms[i].GetComponent<Rigidbody>() == null)
            //                {
            //                    dropPlatform = platforms[i];
            //                    break;
            //                }
            //                if (i == platforms.Length)
            //                {
            //                    return;
            //                }
            //            }
            //        }
            //        dropPlatform.AddComponent<Rigidbody>();
            //        timeBetweenDrop = Random.Range(timeBetweenDropMin, timeBetweenDropMax);
            //        timeTaken = 0;
            //    }  
            //if (boulder.transform.position.z != dropLocation.z)
            //{
            //    boulder.transform.position = new Vector3(boulder.transform.position.x, boulder.transform.position.y, dropLocation.z);
            //    Destroy(boulder);
            //}
            //dropPlatform.AddComponent<Rigidbody>();
            dropPlatform.AddComponent<Rigidbody>();
            Debug.Log("RIPKinematic");
            startdrop = false;

        }
        if (destroyPlatforms)
        {
            for (int i = 0; i < platforms.Length; i++)
            {
                if (platforms[i] != null)
                {
                    if (platforms[i].transform.position.y <= deathHeight)
                    {
                        Destroy(platforms[i]);
                    }
                }
            }

        }

        if (makeKinematic)
        {
            for (int i = 0; i < platforms.Length; i++)
            {
                if (platforms[i].transform.position.y <= deathHeight)
                {
                    platforms[i].GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
    }
}