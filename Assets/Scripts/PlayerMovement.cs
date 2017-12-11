using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public GameObject camera;
    public GameObject movementDirection;
    public ObjectManager objScript;

    public bool controllerMovement;
    public string axisH, axisV;
    public KeyCode moveUp;
    public KeyCode moveDown;
    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode jump;
    public KeyCode dash;
    public KeyCode special;
    public KeyCode pause;

    public float speed;
    public float maxSpeed;
    public float jumpPower;
    public float airborneSpeedDivider;

    public float dashCooldown;
    public float dashForce;
    float dashCurrentCooldown;

    Rigidbody rb;

    public powerups currentPowerup;

    bool shockwaving;
    public float shockwaveRange;
    public float shockwaveForce;

    public bool slimeToggle;

    public float ghostDuration;
    float currentGhostDuration;
    bool ghosting;

    [HideInInspector]
    public float disableDuration;
    bool movementDisabled;

    [HideInInspector]
    public bool pauseReady;

    public AudioClip ghostSound;
    public AudioClip slimeSound;
    public AudioClip shockwaveSound;

    public GameObject shockwaveParticle;
    public GameObject ghostParticle;

    void Start()
    {
        camera = Camera.main.gameObject;
        rb = GetComponent<Rigidbody>();
        shockwaving = false;

        if (shockwaveRange == 0)
            shockwaveRange = 8;
        if (shockwaveForce == 0)
            shockwaveForce = 250;

        slimeToggle = false;
        movementDisabled = false;
    }

    void Update()
    {
        dashCurrentCooldown -= Time.deltaTime;
        currentGhostDuration -= Time.deltaTime;
        disableDuration -= Time.deltaTime;

        if (disableDuration <= 0)
            movementDisabled = false;
        else
            movementDisabled = true;

        if (currentGhostDuration <= 0 && ghosting)
        {
            ghosting = false;
            rb.isKinematic = false;
            GetComponent<SphereCollider>().enabled = true;
            Color playerColor = GetComponent<Renderer>().material.color;
            currentPowerup = powerups.none;
            GetComponent<Ball>().currentPowerup = powerups.none;
            GetComponent<Renderer>().material = GetComponent<Ball>().originalMaterial;
        }

        movementDirection.transform.eulerAngles = new Vector3(0, camera.transform.eulerAngles.y, 0);

        if (Input.GetKeyDown(pause))
        {
            if (objScript.paused)
            {
                if (!pauseReady)
                    pauseReady = true;
                else
                    pauseReady = false;
            }
            else
                objScript.pause();
        }

        if (!objScript.paused)
        {
            if (Input.GetKey(special))
            {
                currentPowerup = GetComponent<Ball>().currentPowerup;
                switch (currentPowerup)
                {
                    case powerups.speedUp: speedUp(); break;
                    case powerups.slowDown: slowDown(); break;
                    case powerups.shockwave: shockwave(); break;
                    case powerups.slime: slime(); break;
                    case powerups.ghost: ghost(); break;
                }
            }

            if (Input.GetKeyUp(special))
            {
                currentPowerup = GetComponent<Ball>().currentPowerup;
                switch (currentPowerup)
                {
                    case powerups.slime: slime(); break;
                }
            }

            Vector2 movementAxes = Vector2.zero;
            if (!controllerMovement)
            {
                if (Input.GetKey(moveUp))
                    movementAxes += new Vector2(1, 0);
                if (Input.GetKey(moveDown))
                    movementAxes -= new Vector2(1, 0);
                if (Input.GetKey(moveLeft))
                    movementAxes -= new Vector2(0, 1);
                if (Input.GetKey(moveRight))
                    movementAxes += new Vector2(0, 1);
            }
            else
            {
                Debug.Log(axisH);
                Debug.Log(axisV);
                if (Mathf.Abs(Input.GetAxis(axisV)) > 0.25)
                    movementAxes -= new Vector2(Input.GetAxis(axisV), 0);
                if (Mathf.Abs(Input.GetAxis(axisH)) > 0.25)
                    movementAxes += new Vector2(0, Input.GetAxis(axisH));
            }

            if (Input.GetKeyDown(dash) && dashCurrentCooldown <= 0 && !movementDisabled)
            {
                dashCurrentCooldown = dashCooldown;
                rb.AddForce(movementAxes.x * movementDirection.transform.forward * dashForce, ForceMode.Acceleration);
                rb.AddForce(movementAxes.y * movementDirection.transform.right * dashForce, ForceMode.Acceleration);
            }

            if (GetComponent<Ball>().grounded)
            {
                if (shockwaving)
                {
                    shockwaving = false;
                    GetComponent<AudioSource>().clip = shockwaveSound;
                    GetComponent<AudioSource>().Play();
                    Instantiate(shockwaveParticle, transform.position - new Vector3(0, -0.5f, 0), Quaternion.identity);
                    foreach (GameObject player in objScript.players)
                    {
                        if (player != gameObject)
                            if (Vector3.Distance(transform.position, player.transform.position) < shockwaveRange)
                                player.GetComponent<Rigidbody>().AddForce(-(transform.position - player.transform.position).normalized / Vector3.Distance(transform.position, player.transform.position) * shockwaveForce);
                    }
                }
                else if (!movementDisabled)
                {
                    rb.AddForce(movementAxes.x * movementDirection.transform.forward * speed * Time.deltaTime, ForceMode.Acceleration);
                    rb.AddForce(movementAxes.y * movementDirection.transform.right * speed * Time.deltaTime, ForceMode.Acceleration);
                }
                if (Input.GetKeyDown(jump))
                {
                    rb.AddForce(movementDirection.transform.up * jumpPower, ForceMode.Acceleration);
                    GetComponent<Ball>().grounded = false;
                }
            }
            else if (!movementDisabled)
            {
                rb.AddForce(movementAxes.x * movementDirection.transform.forward * speed * Time.deltaTime / airborneSpeedDivider, ForceMode.Acceleration);
                rb.AddForce(movementAxes.y * movementDirection.transform.right * speed * Time.deltaTime / airborneSpeedDivider, ForceMode.Acceleration);
            }

            //if (GetComponent<Ball>().grounded)
            rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.z, -maxSpeed, maxSpeed));
        }
    }

    void speedUp()
    {
        rb.velocity *= 2;
        currentPowerup = powerups.none;
        GetComponent<Ball>().currentPowerup = powerups.none;
        transform.GetComponent<Renderer>().material = GetComponent<Ball>().originalMaterial;
    }

    void slowDown()
    {
        rb.velocity /= 4;
        currentPowerup = powerups.none;
        GetComponent<Ball>().currentPowerup = powerups.none;
        transform.GetComponent<Renderer>().material = GetComponent<Ball>().originalMaterial;
    }

    void shockwave()
    {
        if (!GetComponent<Ball>().grounded)
        {
            rb.velocity = new Vector3(0, -15, 0);
            shockwaving = true;
            currentPowerup = powerups.none;
            GetComponent<Ball>().currentPowerup = powerups.none;
            transform.GetComponent<Renderer>().material = GetComponent<Ball>().originalMaterial;
        }
    }

    void slime()
    {
        if (Input.GetKeyUp(special))
        {
            GetComponent<SlimeTrail>().enabled = false;
            return;
        }

        if (!slimeToggle)
        {
            slimeToggle = true;
            GetComponent<SlimeTrail>().resetSlime();
            GetComponent<AudioSource>().clip = slimeSound;
            GetComponent<AudioSource>().Play();
        }

        GetComponent<SlimeTrail>().enabled = true;
    }

    void ghost()
    {
        if (!ghosting)
        {
            currentGhostDuration = ghostDuration;
            GetComponent<AudioSource>().clip = ghostSound;
            GetComponent<AudioSource>().Play();
            Instantiate(ghostParticle, transform.position, Quaternion.identity);
        }
        ghosting = true;
        rb.isKinematic = true;
        GetComponent<SphereCollider>().enabled = false;
        Color playerColor = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = new Color(playerColor.r, playerColor.g, playerColor.b, 0.5f);
    }
}
