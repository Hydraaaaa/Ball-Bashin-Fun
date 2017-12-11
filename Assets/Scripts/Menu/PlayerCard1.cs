using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardState
{
    NONE,
    AI,
    KEYBOARD,
    CONTROLLER
}

public class PlayerCard1 : MonoBehaviour
{
    public int playerNum;

    static bool AIAdded;

    public GameObject[] noneElements;
    public GameObject[] keyboardElements;
    public GameObject[] controllerElements;
    public GameObject[] AIElements;

    public GameObject keyboardJoin;
    public GameObject controllerJoin;
    public GameObject AIButton;

    bool usingKeyboard;
    bool ai;
    int controllerIndex;

    CardState _state;
    public CardState state
    {
        get
        {
            return _state;
        }

        set
        {
            _state = value;

            switch (value)
            {
                case CardState.AI:
                    for (int i = 0; i < noneElements.Length; i++)
                        noneElements[i].SetActive(false);

                    for (int i = 0; i < keyboardElements.Length; i++)
                        keyboardElements[i].SetActive(false);
                    
                    for (int i = 0; i < controllerElements.Length; i++)
                        controllerElements[i].SetActive(false);

                    for (int i = 0; i < AIElements.Length; i++)
                        AIElements[i].SetActive(true);

                    break;

                case CardState.KEYBOARD:
                    for (int i = 0; i < noneElements.Length; i++)
                        noneElements[i].SetActive(false);

                    for (int i = 0; i < AIElements.Length; i++)
                        AIElements[i].SetActive(false);

                    for (int i = 0; i < controllerElements.Length; i++)
                        controllerElements[i].SetActive(false);

                    for (int i = 0; i < keyboardElements.Length; i++)
                        keyboardElements[i].SetActive(true);

                    break;

                case CardState.CONTROLLER:
                    for (int i = 0; i < noneElements.Length; i++)
                        noneElements[i].SetActive(false);

                    for (int i = 0; i < keyboardElements.Length; i++)
                        keyboardElements[i].SetActive(false);

                    for (int i = 0; i < AIElements.Length; i++)
                        AIElements[i].SetActive(false);

                    for (int i = 0; i < controllerElements.Length; i++)
                        controllerElements[i].SetActive(true);

                    break;
                default:
                    for (int i = 0; i < AIElements.Length; i++)
                        AIElements[i].SetActive(false);

                    for (int i = 0; i < keyboardElements.Length; i++)
                        keyboardElements[i].SetActive(false);

                    for (int i = 0; i < controllerElements.Length; i++)
                        controllerElements[i].SetActive(false);

                    for (int i = 0; i < noneElements.Length; i++)
                        noneElements[i].SetActive(true);

                    GameSetup.instance.RemovePlayer(playerNum);
                    if (usingKeyboard)
                    {
                        DeviceManager.instance.keyboardInUse = false;
                        usingKeyboard = false;
                    }
                    else if (!ai)
                        DeviceManager.instance.devicesInUse[controllerIndex] = false;
                    else
                        ai = false;

                    break;
            }
        }
    }

    public void SetNone()
    {
        state = CardState.NONE;
    }

    public void SetAI()
    {
        state = CardState.AI;
        GameSetup.instance.AddPlayer(playerNum);
        GameSetup.instance.GetPlayer(playerNum).isAI = true;
        ai = true;
    }

	void Start ()
    {
        AIAdded = false;
        _state = CardState.NONE;
	}

    void LateUpdate()
    {
        if (AIAdded)
            AIAdded = !AIAdded;
    }
	
	void Update ()
    {
        if (_state == CardState.NONE)
        {

            if (DeviceManager.instance.devicesInUse.Length > 0)
            {
                AIButton.SetActive(true);
                for (int i = 0; i < DeviceManager.instance.devicesInUse.Length; i++)
                {
                    if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + (i + 1) + "Button3")) && !AIAdded)
                    {
                        state = CardState.AI;
                        GameSetup.instance.AddPlayer(playerNum);
                        GameSetup.instance.GetPlayer(playerNum).isAI = true;
                        AIAdded = true;
                        break;
                    }
                }
            }
            else
                AIButton.SetActive(false);

            if (!DeviceManager.instance.keyboardInUse)
            {
                keyboardJoin.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    DeviceManager.instance.keyboardInUse = true;
                    usingKeyboard = true;
                    state = CardState.KEYBOARD;
                    GameSetup.instance.AddPlayer(playerNum);
                    Player player = GameSetup.instance.GetPlayer(playerNum);
                    player.controllerMovement = false;
                    player.up = KeyCode.W;
                    player.down = KeyCode.S;
                    player.left = KeyCode.A;
                    player.right = KeyCode.D;
                    player.jump = KeyCode.LeftShift;
                    player.dash = KeyCode.Space;
                    player.special = KeyCode.E;
                    player.pause = KeyCode.Escape;
                    player.isAI = false;
                }
            }
            else
                keyboardJoin.SetActive(false);

            if (!DeviceManager.instance.devicesAllInUse)
            {
                controllerJoin.SetActive(true);
                for (int i = 0; i < DeviceManager.instance.devicesInUse.Length; i++)
                {
                    if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + (i + 1) + "Button0")) && DeviceManager.instance.devicesInUse[i] == false)
                    {
                        DeviceManager.instance.devicesInUse[i] = true;
                        controllerIndex = 0;
                        state = CardState.CONTROLLER;
                        GameSetup.instance.AddPlayer(playerNum);
                        Player player = GameSetup.instance.GetPlayer(playerNum);
                        player.controllerMovement = true;
                        player.jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + playerNum + "Button0");
                        player.dash = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + playerNum + "Button2");
                        player.special = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + playerNum + "Button1");
                        player.pause = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + playerNum + "Button7");
                        player.isAI = false;
                        break;
                    }
                }
            }
            else
                controllerJoin.SetActive(false);
        }
        else
        {
            keyboardJoin.SetActive(false);
            controllerJoin.SetActive(false);

            if (state == CardState.CONTROLLER)
            {
                if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + (controllerIndex + 1) + "Button2")))
                    state = CardState.NONE;
            }
        }
    }
}
