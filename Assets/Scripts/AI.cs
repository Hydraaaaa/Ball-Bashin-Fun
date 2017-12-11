using UnityEngine;

public class AI : MonoBehaviour
{
    public ObjectManager objScript;
    Rigidbody rb;

    public GameObject movementDirection;

    public float dashCooldown;
    public float dashSpeed;
    float dashCurrentCooldown;

    public float dashClosestRange; //2
    public float dashFurthestRange; //5

    public float powerUpRange; //10

    public float maxTargetDistance;

    public float shockwaveRange; //15
    bool shockwaveFirst = true;
    float shockwaveTimer = 0;
    public float shockwaveUseDelay;
    public float shockwaveForce;

    public float jumpPower;

    public float speed;
    public float maxSpeed;
    public float airborneSpeedDivider;

    //bool dashOn;
    //float timeGhosted;
    bool runAway;
    bool goForplayer;

    public float useSlimeRange; //7.5f
    public float useShockwaveRange; //2.5f
    public float useShockwaveJumpRange;
    public float marbleRunawayRange; //2.5f

    public float ghostPoolCueRange; //0.5f
    public float useGhostRange; //1.5f

    public float bowlingBallDashRange; //2;

    public bool slimeToggle = true;
    Vector2 movementAxes = Vector2.zero;
    public float ghostDuration;
    float currentGhostDuration;
    bool ghosting;

    bool sliming;

    bool shockwaving;

    GameObject closestTarget;

    public powerups currentPowerup;

    public AudioClip ghostSound;
    public AudioClip slimeSound;
    public AudioClip shockwaveSound;

    public GameObject shockwaveParticle;
    public GameObject ghostParticle;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.AddForce(movementDirection.transform.up * jumpPower * Time.deltaTime, ForceMode.Acceleration);
        //GetComponent<Ball>().grounded = false;

    }
    //if (Input.GetKey(moveUp))
    //   movementAxes += new Vector2(1, 0);
    //if (Input.GetKey(moveDown))
    //   movementAxes -= new Vector2(1, 0);
    //if (Input.GetKey(moveLeft))
    //   movementAxes -= new Vector2(0, 1);
    //if (Input.GetKey(moveRight))
    //   movementAxes += new Vector2(0, 1);
    void Update()
    {
        dashCurrentCooldown -= Time.deltaTime;

        //if (!shockwaveFirst && shockwaveTimer < shockwaveUseDelay)
        //{
        //    shockwaveTimer += Time.deltaTime;
        // * Time.deltaTime
        //}
        if (GetComponent<Ball>().grounded)
        {
            shockwaveFirst = true;
        }
        //powerup bools
        if (sliming)
        {
            if (GetComponent<Ball>().currentPowerup == powerups.none)
            {
                GetComponent<SlimeTrail>().resetSlime();
                GetComponent<SlimeTrail>().enabled = false;
                sliming = false;
            }
        }

        if (ghosting)
        {
            currentGhostDuration -= Time.deltaTime;
            if (currentGhostDuration <= 0)
            {
                //timeGhosted = 0;
                ghosting = false;
                rb.isKinematic = false;
                GetComponent<SphereCollider>().enabled = true;
                GetComponent<Ball>().currentPowerup = powerups.none;
                GetComponent<Renderer>().material = GetComponent<Ball>().originalMaterial;
            }
        }

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
                        player.GetComponent<Rigidbody>().AddForce(-(transform.position - player.transform.position).normalized / Vector3.Distance(transform.position, player.transform.position) * shockwaveForce * Time.deltaTime);
            }
        }

        //

        transform.GetChild(0).eulerAngles = Vector3.zero;
        closestTarget = null;
        float targetDistance = maxTargetDistance;
        for (int i = objScript.players.Count - 1; i >= 0; i--)
        {
            if (Vector3.Distance(transform.position, objScript.players[i].transform.position) < targetDistance && objScript.players[i].GetComponent<PlayerMovement>() != null)
            {
                targetDistance = Vector3.Distance(transform.position, objScript.players[i].transform.position);
                if (objScript.players[i].GetComponent<Ball>().grounded)
                {
                    closestTarget = objScript.players[i];
                }
            }
        }
        if (closestTarget == null)
        {
            goForplayer = true;
            if (GetComponent<Ball>().currentPowerup == powerups.none)
            {
                //bool goForplayer = true;
                for (int i = objScript.powerups.Count - 1; i >= 0; i--)
                {
                    if (Vector3.Distance(transform.position, objScript.powerups[i].transform.position) < targetDistance && objScript.powerups[i].GetComponent<Powerup>().alive)
                    { //Powerup.cs changed to make alive bool public
                        targetDistance = Vector3.Distance(transform.position, objScript.powerups[i].transform.position);
                        closestTarget = objScript.powerups[i];
                        goForplayer = false;
                    }
                }
            }
            if (goForplayer)
            {
                for (int i = objScript.players.Count - 1; i >= 0; i--)
                {
                    if (Vector3.Distance(transform.position, objScript.players[i].transform.position) < targetDistance && objScript.players[i] != gameObject)
                    {
                        targetDistance = Vector3.Distance(transform.position, objScript.players[i].transform.position);
                        closestTarget = objScript.players[i];
                    }
                }
            }
        }
        //use dash
        if (closestTarget != null)
        {
            if (Vector3.Distance(transform.position, closestTarget.transform.position) < dashFurthestRange && Vector3.Distance(transform.position, closestTarget.transform.position) > dashClosestRange && dashCurrentCooldown <= 0 && goForplayer)
            {
                rb.velocity *= dashSpeed;
                dashCurrentCooldown = dashCooldown;
            }
        }
        //use powerup

        //if (GetComponent<Ball>().currentPowerup == powerups.none)
        //{
        //    for (int i = objScript.powerups.Count - 1; i >= 0; i--)
        //    {
        //        if (Vector3.Distance(transform.position, objScript.powerups[i].transform.position) < targetDistance && objScript.powerups[i].GetComponent<Powerup>().alive)
        //        { //Powerup.cs changed to make alive bool public
        //            targetDistance = Vector3.Distance(transform.position, objScript.powerups[i].transform.position);
        //            closestTarget = objScript.powerups[i];
        //        }
        //    }
        //}

        //finite state machines
        if (closestTarget != null)
        {
            if (Vector3.Distance(transform.position, closestTarget.transform.position) < powerUpRange)
            {
                currentPowerup = GetComponent<Ball>().currentPowerup;
                switch (currentPowerup)
                {
                    case powerups.none:
                        {
                            runAway = false;
                            transform.GetComponent<Renderer>().material = GetComponent<Ball>().originalMaterial;
                            break;
                        }
                    case powerups.speedUp: speedUp(); break;
                    case powerups.slowDown: slowDown(); break;
                    case powerups.bowlingBall:
                        {
                            if (Vector3.Distance(transform.position, closestTarget.transform.position) < bowlingBallDashRange && goForplayer)
                            {
                                if (dashCurrentCooldown <= 0)
                                {
                                    rb.velocity *= dashSpeed;
                                    dashCurrentCooldown = dashCooldown;
                                }
                            }
                            break;
                        }
                    case powerups.marble:
                        {
                            if (goForplayer && Vector3.Distance(transform.position, closestTarget.transform.position) < marbleRunawayRange)
                            {
                                runAway = true;
                                if (dashCurrentCooldown <= 0)
                                {
                                    rb.velocity *= dashSpeed;
                                    dashCurrentCooldown = dashCooldown;
                                }
                            }
                            else
                            {
                                runAway = false;
                            }
                            break;
                        }
                    case powerups.shockwave:
                        {
                            if (Vector3.Distance(transform.position, closestTarget.transform.position) < useShockwaveJumpRange)
                                if (GetComponent<Ball>().grounded)
                                {
                                    rb.AddForce(movementDirection.transform.up * jumpPower * Time.deltaTime, ForceMode.Acceleration);
                                    GetComponent<Ball>().grounded = false;
                                    break;
                                }
                            if (Vector3.Distance(transform.position, closestTarget.transform.position) < useShockwaveRange)
                                shockwave();
                            break;
                        }
                    case powerups.slime:
                        {
                            if (Vector3.Distance(transform.position, closestTarget.transform.position) < useSlimeRange)
                                slime();
                            break;
                        }
                    case powerups.ghost:
                        {
                            //if pool cue is close to ai
                            if (Vector3.Distance(transform.position, objScript.GetComponent<PoolCues>().poolCue.transform.position) < ghostPoolCueRange)
                                ghost();
                            //if another player is close to ai
                            if (Vector3.Distance(transform.position, closestTarget.transform.position) < useGhostRange)
                                ghost();
                            break;
                        }
                }
            }
        }


        if (!objScript.paused)
        {
            if (closestTarget != null)
            {
                if (Vector3.Distance(transform.position, closestTarget.transform.position) < 0.1f)
                {
                    closestTarget.transform.position = new Vector3(closestTarget.transform.position.x - 1, closestTarget.transform.position.y - 1, closestTarget.transform.position.z - 1);
                }
                if (GetComponent<Ball>().grounded)
                    if (runAway)
                    {
                        rb.AddForce(-Vector3.Normalize(closestTarget.transform.position - transform.position) * speed * Time.deltaTime, ForceMode.Acceleration);
                    }
                    else
                    {
                        rb.AddForce(Vector3.Normalize(closestTarget.transform.position - transform.position) * speed * Time.deltaTime, ForceMode.Acceleration);
                    }
                else
                if (!shockwaveFirst)
                {
                    if (runAway)
                    {
                        rb.AddForce(-Vector3.Normalize(closestTarget.transform.position - transform.position) * speed * Time.deltaTime / airborneSpeedDivider, ForceMode.Acceleration);
                    }
                    else
                    {
                        rb.AddForce(Vector3.Normalize(closestTarget.transform.position - transform.position) * speed * Time.deltaTime / airborneSpeedDivider, ForceMode.Acceleration);
                    }
                }
            }
        }

        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.z, -maxSpeed, maxSpeed));
    }

    #region Powerup Functions
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
        //if (shockwaveFirst)
        //{
        //    if (GetComponent<Ball>().grounded)
        //    {
        //        rb.AddForce(movementDirection.transform.up * jumpPower, ForceMode.Acceleration);
        //        GetComponent<Ball>().grounded = false;
        //        return;
        //    }
        //    shockwaveFirst = false;
        //}

        //if (!shockwaveFirst && shockwaveTimer < shockwaveUseDelay)
        //{
        //    shockwaveTimer += Time.deltaTime;
        //}

        //^ Jump

        //if (GetComponent<Ball>().grounded)
        //{
        //    rb.AddForce(movementDirection.transform.up * jumpPower, ForceMode.Acceleration);
        //    shockwaveFirst = true;
        //    GetComponent<Ball>().grounded = false;
        //    return;
        //}

        //if (transform.position.y <= 0)
        //{
        //    return;
        //}

        //if (!shockwaveFirst && shockwaveTimer >= shockwaveUseDelay)
        {
            if (!GetComponent<Ball>().grounded)
            {
                rb.velocity = new Vector3(0, -15, 0);
                shockwaving = true;
                //shockwaveFirst = true;
                currentPowerup = powerups.none;
                GetComponent<Ball>().currentPowerup = powerups.none;
                transform.GetComponent<Renderer>().material = GetComponent<Ball>().originalMaterial;
            }
            //shockwaveFirst = true;
            //shockwaveTimer = 0;
        }
    }

    void slime()
    {
        //if (Input.GetKeyUp(special))
        //{
        //    GetComponent<SlimeTrail>().enabled = false;
        //    return;
        //}

        if (!slimeToggle)
        {
            slimeToggle = true;
            GetComponent<SlimeTrail>().resetSlime();
            GetComponent<AudioSource>().clip = slimeSound;
            GetComponent<AudioSource>().Play();
        }

        GetComponent<SlimeTrail>().enabled = true;
        sliming = true;
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
        //while (ghosting == true)
        //{
        //    currentGhostDuration -= Time.deltaTime;
        //    if (currentGhostDuration <= 0)
        //    {
        //        //timeGhosted = 0;
        //        ghosting = false;
        //        rb.isKinematic = false;
        //        GetComponent<SphereCollider>().enabled = true;
        //        GetComponent<Ball>().currentPowerup = powerups.none;
        //        GetComponent<Renderer>().material = GetComponent<Ball>().originalMaterial;
        //    }
        //}
    }
    #endregion
}
