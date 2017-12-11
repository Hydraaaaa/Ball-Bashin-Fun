using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Powerup2 : MonoBehaviour
{
    powerups powerup;
    float deathTimer;
    bool alive;
    public bool beginSpawned;
    public float respawnTime;

    public bool spawnSpeedUp;
    public bool spawnSlowDown;
    public bool spawnShockwave;
    public bool spawnBomb;
    public bool spawnSlime;
    public bool spawnBowlingBall;
    public bool spawnMarble;
    public bool spawnGhost;

    public Material speedUp;
    public Material slowDown;
    public Material shockwave;
    public Material bomb;
    public Material slime;
    public Material bowlingBall;
    public Material marble;
    public Material ghost;

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
            if (other.CompareTag("Player") && other.GetComponent<Ball>().currentPowerup == powerups.none)
            {
                other.GetComponent<Ball>().currentPowerup = powerup;

                switch (powerup)
                {
                    case powerups.speedUp:
                        other.GetComponent<Renderer>().material = Resources.Load("Materials/Powerups/SpeedUp" + other.GetComponent<Ball>().num, typeof(Material)) as Material;
                        break;
                    case powerups.slowDown:
                        other.GetComponent<Renderer>().material = Resources.Load("Materials/Powerups/SlowDown" + other.GetComponent<Ball>().num, typeof(Material)) as Material;
                        break;
                    case powerups.shockwave:
                        other.GetComponent<Renderer>().material = Resources.Load("Materials/Powerups/IronBall" + other.GetComponent<Ball>().num, typeof(Material)) as Material;
                        break;
                    case powerups.slime:
                        other.GetComponent<Renderer>().material = Resources.Load("Materials/Powerups/Slime" + other.GetComponent<Ball>().num, typeof(Material)) as Material;
                        break;
                    case powerups.bowlingBall:
                        other.GetComponent<Renderer>().material = Resources.Load("Materials/Powerups/BowlingBall" + other.GetComponent<Ball>().num, typeof(Material)) as Material;
                        break;
                    case powerups.marble:
                        other.GetComponent<Renderer>().material = Resources.Load("Materials/Powerups/Marble" + other.GetComponent<Ball>().num, typeof(Material)) as Material;
                        break;
                    case powerups.ghost:
                        other.GetComponent<Renderer>().material = Resources.Load("Materials/Powerups/Ghost" + other.GetComponent<Ball>().num, typeof(Material)) as Material;
                        break;
                }

                alive = false;
                GetComponent<MeshRenderer>().enabled = false;
                deathTimer = respawnTime;
            }
    }

    void Spawn()
    {
        GetComponent<MeshRenderer>().enabled = true;

        while (!alive)
        {
            if (!spawnShockwave && !spawnSlowDown && !spawnSpeedUp && !spawnBomb && !spawnSlime && !spawnBowlingBall && !spawnMarble && !spawnGhost)
            {
                GetComponent<Renderer>().material.color = new Color(0, 0, 0);
                alive = true;
                return;
            }
            powerup = (powerups)System.Enum.Parse(typeof(powerups), Random.Range(1, 9).ToString());
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
                case powerups.bomb:
                    alive = spawnBomb;
                    GetComponent<Renderer>().material = bomb;
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
            }

        }
    }
}
