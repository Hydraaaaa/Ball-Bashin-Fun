using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour
{
    public GameObject[] playerSpawns;
    public List<GameObject> players = new List<GameObject>();
    public List<GameObject> powerups = new List<GameObject>();

    public float endDelay;
    float endTimer;

    public KeyCode universalQuit1;
    public KeyCode universalQuit2;
    public KeyCode universalQuit3;

    public GameObject victoryUI;
    bool victoryOnce;

    public bool paused;

    public GameObject pauseUI;

    public float timeElapsed;

    void Awake()
    {
        playerSpawns = GameObject.FindGameObjectsWithTag("PlayerSpawn");
        powerups.AddRange(GameObject.FindGameObjectsWithTag("Powerup"));
    }

    void Start()
    {
        Camera.main.GetComponent<StaticCamera>().library = GetComponent<ObjectManager>();
        endTimer = endDelay;
        victoryOnce = false;

        paused = false;
        Time.timeScale = 1;
        GameObject temp = Instantiate(pauseUI) as GameObject;
        pauseUI = temp;

        timeElapsed = 0;
    }

	void Update()
    {
        if (Input.GetKey(universalQuit1) && Input.GetKey(universalQuit2) && Input.GetKey(universalQuit3))
        {
            SceneManager.LoadScene("Menu");
        }

        timeElapsed += Time.deltaTime;

        if (paused)
        {
            bool resume = true;
            for (int i = players.Count - 1; i >= 0; i--)
            {

                if (players[i].GetComponent<AI>() != null)
                    continue;

                if (players[i].GetComponent<PlayerMovement>().pauseReady)
                    pauseUI.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                else
                {
                    resume = false;
                    pauseUI.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
                }
            }

            if (resume)
            {
                paused = false;
                Time.timeScale = 1;
                pauseUI.SetActive(false);
            }
        }

        for (int i = players.Count - 1; i >= 0; i--)
        {
            if (players[i] == null)
            {
                players.Remove(players[i]);
                continue;
            }
            if (players[i].GetComponent<Ball>().isAlive == false && players.Count > 1)
            {
                players.Remove(players[i]);
                continue;
            }
        }
        
        for (int i = powerups.Count - 1; i >= 0; i--)
        {
            if (powerups[i] == null)
                powerups.Remove(powerups[i]);
        }

        if (players.Count <= 1)
        {
            if (!victoryOnce)
            {
                
                GameObject ui = Instantiate(victoryUI) as GameObject;
                ui.transform.GetChild(0).GetComponent<Text>().text = players[0].GetComponent<Ball>().name + " Wins";
                victoryOnce = true;
            }

            endTimer -= Time.deltaTime;
            if (endTimer <= 0)
                SceneManager.LoadScene("Menu");
        }
    }

    public void pause()
    {
        Time.timeScale = 0;
        paused = true;
        for (int i = players.Count - 1; i >= 0; i--)
        {
            if (players[i].GetComponent<PlayerMovement>() != null)
                players[i].GetComponent<PlayerMovement>().pauseReady = false;
        }

        pauseUI.SetActive(true);
    }
}