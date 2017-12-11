using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    [HideInInspector]public string name;
    [HideInInspector]public int num;

    [HideInInspector]public bool isAlive;
    [HideInInspector]public powerups currentPowerup;

    public bool grounded;

    bool powerupToggle;

    float currentDuration;

    [HideInInspector]public Material originalMaterial;
    float originalSpeed;
    float originalDashForce;
    float originalMass;
    Vector3 originalScale;
    
    public float bowlingBallDuration;
    public float bowlingBallSpeed;
    public float bowlingBallMass;
    public float bowlingBallScale;

    public float marbleDuration;
    public float marbleSpeed;
    public float marbleMass;
    public float marbleScale;

    void Awake()
    {
        grounded = false;
        isAlive = true;
        currentPowerup = powerups.none;
        powerupToggle = false;

        if (GetComponent<PlayerMovement>() != null)
        {
            originalSpeed = GetComponent<PlayerMovement>().speed;
            originalDashForce = GetComponent<PlayerMovement>().dashForce;
        }
        else
        {
            originalSpeed = GetComponent<AI>().speed;
            originalDashForce = 1;
        }
        

        originalMass = GetComponent<Rigidbody>().mass;
        originalScale = transform.localScale;
    }

    void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
    }
	
	void Update()
    {
        if (transform.position.y < -6f)
        {
            isAlive = false;
            if (GetComponent<PlayerMovement>() != null)
                GetComponent<PlayerMovement>().enabled = false;
            else if (GetComponent<AI>() != null)
                GetComponent<AI>().enabled = false;
        }

        currentDuration -= Time.deltaTime;

        if (currentPowerup == powerups.bowlingBall)
        {
            if (!powerupToggle)
            {
                powerupToggle = true;
                currentDuration = bowlingBallDuration;

                if (GetComponent<PlayerMovement>() != null)
                {
                    GetComponent<PlayerMovement>().speed *= bowlingBallSpeed;
                    GetComponent<PlayerMovement>().dashForce *= bowlingBallSpeed;
                }
                else
                    GetComponent<AI>().speed *= bowlingBallSpeed;

                GetComponent<Rigidbody>().mass = bowlingBallMass;
                transform.localScale *= bowlingBallScale;
            }
            else if (currentDuration <= 0)
            {
                currentPowerup = powerups.none;
                powerupToggle = false;

                GetComponent<Renderer>().material = originalMaterial;
                if (GetComponent<PlayerMovement>() != null)
                {
                    GetComponent<PlayerMovement>().speed = originalSpeed;
                    GetComponent<PlayerMovement>().dashForce = originalDashForce;
                }
                else
                    GetComponent<AI>().speed = originalSpeed;
                
                GetComponent<Rigidbody>().mass = originalMass;
                transform.localScale = originalScale;
            }
        }
        else if (currentPowerup == powerups.marble)
        {
            if (!powerupToggle)
            {
                powerupToggle = true;
                currentDuration = marbleDuration;

                if (GetComponent<PlayerMovement>() != null)
                {
                    GetComponent<PlayerMovement>().speed *= marbleSpeed;
                    GetComponent<PlayerMovement>().dashForce *= marbleSpeed;
                }
                else
                    GetComponent<AI>().speed *= marbleSpeed;

                GetComponent<Rigidbody>().mass = marbleMass;
                transform.localScale *= marbleScale;
            }
            else if (currentDuration <= 0)
            {
                currentPowerup = powerups.none;
                powerupToggle = false;

                GetComponent<Renderer>().material = originalMaterial;
                if (GetComponent<PlayerMovement>() != null)
                {
                    GetComponent<PlayerMovement>().speed = originalSpeed;
                    GetComponent<PlayerMovement>().dashForce = originalDashForce;
                }
                else
                    GetComponent<AI>().speed = originalSpeed;

                GetComponent<Rigidbody>().mass = originalMass;
                transform.localScale = originalScale;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Bomb") && !other.CompareTag("Flower"))
            grounded = true;

        if (other.CompareTag("DeathTrigger"))
        {
            isAlive = false;
            if (GetComponent<PlayerMovement>() != null)
                GetComponent<PlayerMovement>().enabled = false;
            else if (GetComponent<AI>() != null)
                GetComponent<AI>().enabled = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Bomb") && !other.CompareTag("Flower"))
            grounded = true;

        if (other.CompareTag("DeathTrigger"))
        {
            isAlive = false;
            if (GetComponent<PlayerMovement>() != null)
                GetComponent<PlayerMovement>().enabled = false;
            else if (GetComponent<AI>() != null)
                GetComponent<AI>().enabled = false;
        }
    }

    void OnTriggerExit()
    {
        grounded = false;
    }
}
