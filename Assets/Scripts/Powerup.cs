using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum powerups
{
    none,
    speedUp,
    slowDown,
    shockwave,
    slime,
    bowlingBall,
    marble,
    ghost,
    bomb,
    rain,
    platformDrop
}

public class Powerup : MonoBehaviour
{
    powerups powerup;
    float deathTimer;
    public bool alive;
    public bool beginSpawned;
    public float respawnTime;

    public bool spawnSpeedUp;
    public bool spawnSlowDown;
    public bool spawnShockwave;
    public bool spawnSlime;
    public bool spawnBowlingBall;
    public bool spawnMarble;
    public bool spawnGhost;
    public bool spawnBomb;
    public bool spawnRain;
    public bool spawnPlatformDrop;

    public Material speedUp;
    public Material slowDown;
    public Material shockwave;
    public Material slime;
    public Material bowlingBall;
    public Material marble;
    public Material ghost;
    public Material bomb;
    public Material rain;
    public Material platformDrop;

    public AudioClip speedUpSound;
    public AudioClip slowDownSound;
    public AudioClip shockwaveSound;
    public AudioClip slimeSound;
    public AudioClip bowlingBallSound;
    public AudioClip marbleSound;
    public AudioClip ghostSound;
    public AudioClip bombSound;
    public AudioClip rainSound;
    public AudioClip platformDropSound;

    public GameObject rainGod;
    public GameObject boulderGod;
    public GameObject bombObject;

    public GameObject particle;

    void Start()
    {
        alive = false;
        if (beginSpawned)
            Spawn();
        else
        {
            GetComponent<MeshRenderer>().enabled = false;
            deathTimer = respawnTime;
        }
    }

    void Update()
    {
        if (alive)
            transform.Rotate(new Vector3(50f, 50f, 50f) * Time.deltaTime);
        else if (deathTimer <= 0)
            Spawn();
        else
            deathTimer -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (alive)
        {
            if (other.CompareTag("Player"))
            {
                if (other.GetComponent<Ball>().currentPowerup == powerups.none)
                {

                    switch (powerup)
                    {
                        case powerups.speedUp:
                            other.GetComponent<Renderer>().material = Resources.Load("Materials/Powerups/SpeedUp" + other.GetComponent<Ball>().num, typeof(Material)) as Material;
                            other.GetComponent<Ball>().currentPowerup = powerup;
                            other.GetComponent<AudioSource>().clip = speedUpSound;
                            other.GetComponent<AudioSource>().Play();
                            break;
                        case powerups.slowDown:
                            other.GetComponent<Renderer>().material = Resources.Load("Materials/Powerups/SlowDown" + other.GetComponent<Ball>().num, typeof(Material)) as Material;
                            other.GetComponent<Ball>().currentPowerup = powerup;
                            other.GetComponent<AudioSource>().clip = slowDownSound;
                            other.GetComponent<AudioSource>().Play();
                            break;
                        case powerups.shockwave:
                            other.GetComponent<Renderer>().material = Resources.Load("Materials/Powerups/IronBall" + other.GetComponent<Ball>().num, typeof(Material)) as Material;
                            other.GetComponent<Ball>().currentPowerup = powerup;
                            other.GetComponent<AudioSource>().clip = shockwaveSound;
                            other.GetComponent<AudioSource>().Play();
                            break;
                        case powerups.slime:
                            other.GetComponent<Renderer>().material = Resources.Load("Materials/Powerups/Slime" + other.GetComponent<Ball>().num, typeof(Material)) as Material;
                            other.GetComponent<Ball>().currentPowerup = powerup;
                            other.GetComponent<AudioSource>().clip = slimeSound;
                            other.GetComponent<AudioSource>().Play();
                            break;
                        case powerups.bowlingBall:
                            other.GetComponent<Renderer>().material = Resources.Load("Materials/Powerups/BowlingBall" + other.GetComponent<Ball>().num, typeof(Material)) as Material;
                            other.GetComponent<Ball>().currentPowerup = powerup;
                            other.GetComponent<AudioSource>().clip = bowlingBallSound;
                            other.GetComponent<AudioSource>().Play();
                            break;
                        case powerups.marble:
                            other.GetComponent<Renderer>().material = Resources.Load("Materials/Powerups/Marble" + other.GetComponent<Ball>().num, typeof(Material)) as Material;
                            other.GetComponent<Ball>().currentPowerup = powerup;
                            other.GetComponent<AudioSource>().clip = marbleSound;
                            other.GetComponent<AudioSource>().Play();
                            break;
                        case powerups.ghost:
                            other.GetComponent<Renderer>().material = Resources.Load("Materials/Powerups/Ghost" + other.GetComponent<Ball>().num, typeof(Material)) as Material;
                            other.GetComponent<Ball>().currentPowerup = powerup;
                            other.GetComponent<AudioSource>().clip = ghostSound;
                            other.GetComponent<AudioSource>().Play();
                            break;
                    }

                    alive = false;
                    GetComponent<MeshRenderer>().enabled = false;
                    deathTimer = respawnTime;
                    Instantiate(particle, transform.position, Quaternion.identity);
                }

                switch (powerup)
                {
                    case powerups.bomb:
                        alive = false;
                        GetComponent<MeshRenderer>().enabled = false;
                        deathTimer = respawnTime;

                        GameObject newBomb = Instantiate(bombObject) as GameObject;
                        newBomb.GetComponent<Bomb>().currentPlayer = other.gameObject;
                        other.GetComponent<AudioSource>().clip = bombSound;
                        other.GetComponent<AudioSource>().Play();
                        Instantiate(particle, transform.position, Quaternion.identity);

                        break;
                    case powerups.rain:
                        alive = false;
                        GetComponent<MeshRenderer>().enabled = false;
                        deathTimer = respawnTime;

                        rainGod.GetComponent<Rain>().enabled = true;
                        rainGod.GetComponent<PuddleDropSpawner>().enabled = true;
                        other.GetComponent<AudioSource>().clip = rainSound;
                        other.GetComponent<AudioSource>().Play();
                        Instantiate(particle, transform.position, Quaternion.identity);

                        break;
                    case powerups.platformDrop:
                        GetComponent<MeshRenderer>().enabled = false;
                        deathTimer = respawnTime;

                        boulderGod.GetComponent<PlatformDrop>().enabled = true;
                        other.GetComponent<AudioSource>().clip = platformDropSound;
                        other.GetComponent<AudioSource>().Play();
                        Instantiate(particle, transform.position, Quaternion.identity);
                        break;
                }

            }
        }
    }

    void Spawn()
    {
        GetComponent<MeshRenderer>().enabled = true;

        while (!alive)
        {
            if (!spawnShockwave && !spawnSlowDown && !spawnSpeedUp && !spawnSlime && !spawnBowlingBall && !spawnMarble && !spawnGhost && !spawnBomb && !spawnRain && !spawnPlatformDrop)
            {
                GetComponent<Renderer>().material.color = new Color(0, 0, 0);
                alive = true;
                return;
            }
            powerup = (powerups)System.Enum.Parse(typeof(powerups), Random.Range(1, 11).ToString());
            switch (powerup)
            {
                case powerups.speedUp:
                    alive = spawnSpeedUp;
                    GetComponent<Renderer>().material = speedUp;
                    break;
                case powerups.slowDown:
                    alive = spawnSlowDown;
                    GetComponent<Renderer>().material = slowDown;
                    break;
                case powerups.shockwave:
                    alive = spawnShockwave;
                    GetComponent<Renderer>().material = shockwave;
                    break;
                case powerups.slime:
                    alive = spawnSlime;
                    GetComponent<Renderer>().material = slime;
                    break;
                case powerups.bowlingBall:
                    alive = spawnBowlingBall;
                    GetComponent<Renderer>().material = bowlingBall;
                    break;
                case powerups.marble:
                    alive = spawnMarble;
                    GetComponent<Renderer>().material = marble;
                    break;
                case powerups.ghost:
                    alive = spawnGhost;
                    GetComponent<Renderer>().material = ghost;
                    break;
                case powerups.bomb:
                    alive = spawnBomb;
                    GetComponent<Renderer>().material = bomb;
                    break;
                case powerups.rain:
                    alive = spawnRain;
                    GetComponent<Renderer>().material = rain;
                    break;
                case powerups.platformDrop:
                    alive = spawnPlatformDrop;
                    GetComponent<Renderer>().material = platformDrop;
                    break;
            }

        }
    }
}
