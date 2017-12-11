using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class Player
{
    public Player(int i = 1, bool ai = false)
    {
        num = i;
        isAI = ai;
        if (i == 1)
        {
            up = 0;
            down = 0;
            left = 0;
            right = 0;
            jump = KeyCode.Joystick1Button0;
            dash = KeyCode.Joystick1Button2;
            special = KeyCode.Joystick1Button1;
            pause = KeyCode.Joystick1Button4;
            name = "Player 1";
            playerColour = Resources.Load("Materials/Player1", typeof(Material)) as Material;

            controllerNum = 0;
        }
        else if (i == 2)
        {
            up = 0;
            down = 0;
            left = 0;
            right = 0;
            jump = KeyCode.Joystick2Button0;
            dash = KeyCode.Joystick2Button2;
            special = KeyCode.Joystick2Button1;
            pause = KeyCode.Joystick2Button4;
            name = "Player 2";
            playerColour = Resources.Load("Materials/Player2", typeof(Material)) as Material;

            controllerNum = 1;
        }
        else if (i == 3)
        {
            up = 0;
            down = 0;
            left = 0;
            right = 0;
            jump = KeyCode.Joystick3Button0;
            dash = KeyCode.Joystick3Button2;
            special = KeyCode.Joystick3Button1;
            pause = KeyCode.Joystick3Button4;
            name = "Player 3";
            playerColour = Resources.Load("Materials/Player3", typeof(Material)) as Material;

            controllerNum = 2;
        }
        else if (i == 4)
        {
            up = 0;
            down = 0;
            left = 0;
            right = 0;
            jump = KeyCode.Joystick4Button0;
            dash = KeyCode.Joystick4Button2;
            special = KeyCode.Joystick4Button1;
            pause = KeyCode.Joystick4Button4;
            name = "Player 4";
            playerColour = Resources.Load("Materials/Player4", typeof(Material)) as Material;

            controllerNum = 3;
        }
    }

    public int num;
    public bool isAI;
    public bool controllerMovement;
    public int controllerNum;
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode dash;
    public KeyCode special;
    public KeyCode pause;

    public string name;

    public Material playerColour;
    public Texture2D playerClash;
}

public class GameSetup : MonoBehaviour
{
    public static GameSetup instance;

    public GameObject playerPrefab;
    public GameObject aiPrefab;

    public GameObject objectManager;

    public List<Player> players = new List<Player>();

    public string[] levels;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Menu"))
        {
            GameObject objManager = Instantiate(objectManager) as GameObject;

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("PoolTable"))
                objManager.GetComponent<PoolCues>().enabled = true;

            ObjectManager objScript = objManager.GetComponent<ObjectManager>();
            for (int i = 0; i < players.Count; i++)
            {
                GameObject player;
                if (players[i].isAI == false)
                {
                    player = Instantiate(playerPrefab, objScript.playerSpawns[i].transform.position, objScript.playerSpawns[i].transform.rotation) as GameObject;

                    PlayerMovement playerScript = player.GetComponent<PlayerMovement>();
                    playerScript.controllerMovement = players[i].controllerMovement;
                    if (players[i].controllerMovement)
                    {
                        playerScript.axisH = "Horizontal " + i;
                        playerScript.axisV = "Vertical " + i;
                    }
                    else
                    {
                        playerScript.moveUp = players[i].up;
                        playerScript.moveDown = players[i].down;
                        playerScript.moveLeft = players[i].left;
                        playerScript.moveRight = players[i].right;
                    }

                    playerScript.jump = players[i].jump;
                    playerScript.dash = players[i].dash;
                    playerScript.special = players[i].special;
                    playerScript.pause = players[i].pause;
                    playerScript.objScript = objScript;
                }
                else
                {
                    player = Instantiate(aiPrefab, objScript.playerSpawns[i].transform.position, objScript.playerSpawns[i].transform.rotation) as GameObject;
                    player.GetComponent<AI>().objScript = objScript;
                }
                
                player.GetComponent<Renderer>().material = players[i].playerColour;
                player.GetComponent<Ball>().name = players[i].name;
                player.GetComponent<Ball>().num = players[i].num;


                objScript.players.Add(player);
            }
            Destroy(gameObject);
        }
    }

    public void Play(string levelToLoad)
    {
        if (levelToLoad == "Random")
            levelToLoad = levels[Random.Range(0, levels.Length)];

        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(levelToLoad);
    }

    public void AddPlayer(int index, bool ai = false)
    {
        players.Add(new Player(index, ai));
    }

    public Player GetPlayer(int index)
    {
        foreach (Player player in players)
        {
            if (player.num == index)
                return player;
        }
        return null;
    }

    public void RemovePlayer(int index)
    {
        foreach (Player player in players)
        {
            if (player.num == index)
            {
                players.Remove(player);
                return;
            }
        }
    }

    public void SetController(int index, bool value)
    {
        foreach (Player player in players)
        {
            if (player.num == index)
                player.controllerMovement = value;
        }
    }

    public void SetWhichController(int index, int value)
    {
        foreach (Player player in players)
        {
            if (player.num == index)
                player.controllerNum = value;
        }
    }
}
