using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject menuScreen;
    public GameObject playerScreen;
    public GameObject creditsScreen;

    public GameObject playerControllerObject;

    public GameObject[] menuButtons;
    int currentMenuButton;

    public GameObject[] levels;
    int currentLevel;

    GameObject controllerTarget;

    public float switchInterval;
    float switchTimer;

    public GameObject playButton;

    bool dropdown;
    public List<GameObject> dropdownOptions;
    int currentDropdownOption;

    [HideInInspector]
    public bool binding;

    string selectedLevel;

    void Start()
    {
        selectedLevel = "PoolTable";
        Menu();
        binding = false;
    }

    void Update()
    {
        switchTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.JoystickButton0) && !binding)
        {
            if (controllerTarget.GetComponent<Button>() != null)
                controllerTarget.GetComponent<Button>().onClick.Invoke();
            else if (controllerTarget.GetComponent<Toggle>() != null)
                controllerTarget.GetComponent<Toggle>().isOn = !controllerTarget.GetComponent<Toggle>().isOn;
            else if (controllerTarget.GetComponent<Dropdown>() != null)
            {
                if (controllerTarget.transform.FindChild("Dropdown List") == null)
                {
                    controllerTarget.GetComponent<Dropdown>().Show();
                    dropdownOptions = new List<GameObject>();
                    for (int i = 0; i < controllerTarget.transform.FindChild("Dropdown List").GetChild(0).GetChild(0).childCount; i++)
                    {
                        dropdownOptions.Add(controllerTarget.transform.FindChild("Dropdown List").GetChild(0).GetChild(0).GetChild(i).gameObject);
                        dropdownOptions[i].transform.GetChild(0).gameObject.AddComponent<Outline>();
                        dropdownOptions[i].transform.GetChild(0).GetComponent<Outline>().effectColor = new Color(255, 255, 0);
                        dropdownOptions[i].transform.GetChild(0).GetComponent<Outline>().effectDistance = new Vector2(2, -2);
                    }
                    currentDropdownOption = 0;
                    dropdown = true;

                    dropdownOptions.RemoveAt(0);
                }
                else
                {
                    controllerTarget.GetComponent<Dropdown>().value = currentDropdownOption;
                    controllerTarget.GetComponent<Dropdown>().Hide();

                    dropdown = false;
                }
            }
        }

        if (menuScreen.activeInHierarchy)
        {
            if (switchTimer <= 0)
            {
                for (int i = 0; i < Input.GetJoystickNames().Length; i++)
                {
                    if (Input.GetAxis("Horizontal " + i) >= 0.8f)
                    {
                        switchTimer = switchInterval;

                        if (!dropdown)
                        {
                            controllerTarget.GetComponent<Outline>().enabled = false;

                            currentMenuButton++;
                            if (currentMenuButton >= menuButtons.Length)
                                currentMenuButton = 0;

                            controllerTarget = menuButtons[currentMenuButton];

                            controllerTarget.GetComponent<Outline>().enabled = true;
                        }
                    }
                    else if (Input.GetAxis("Horizontal " + i) <= -0.8f)
                    {
                        switchTimer = switchInterval;

                        if (!dropdown)
                        {
                            controllerTarget.GetComponent<Outline>().enabled = false;

                            currentMenuButton--;
                            if (currentMenuButton < 0)
                                currentMenuButton = menuButtons.Length - 1;

                            controllerTarget = menuButtons[currentMenuButton];

                            controllerTarget.GetComponent<Outline>().enabled = true;
                        }
                    }
                }
            }
        }
        else if (playerScreen.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton1) && !binding)
                Menu();

            if (switchTimer <= 0)
            {
                bool[] devicesInUse = DeviceManager.instance.devicesInUse;
                for (int i = 0; i < devicesInUse.Length; i++)
                {
                    if (devicesInUse[i])
                    {
                        if (Input.GetAxisRaw("Horizontal " + i) >= 0.8f)
                        {
                            switchTimer = switchInterval;

                            currentLevel++;
                            if (currentLevel >= levels.Length)
                                currentLevel = 0;

                           if (controllerTarget.GetComponent<Toggle>() == null)
                               controllerTarget.GetComponent<Outline>().enabled = false;
                           else
                               controllerTarget.transform.GetChild(0).GetComponent<Outline>().enabled = false;
                           
                           controllerTarget = levels[currentLevel];
                           
                           if (controllerTarget.GetComponent<Toggle>() == null)
                               controllerTarget.GetComponent<Outline>().enabled = true;
                           else
                               controllerTarget.transform.GetChild(0).GetComponent<Outline>().enabled = true;
                        }
                        else if (Input.GetAxisRaw("Horizontal " + i) <= -0.8f)
                        {
                            switchTimer = switchInterval;

                            currentLevel--;
                            if (currentLevel < 0)
                                currentLevel = levels.Length - 1;

                            if (controllerTarget.GetComponent<Toggle>() == null)
                                controllerTarget.GetComponent<Outline>().enabled = false;
                            else
                                controllerTarget.transform.GetChild(0).GetComponent<Outline>().enabled = false;

                            controllerTarget = levels[currentLevel];

                            if (controllerTarget.GetComponent<Toggle>() == null)
                                controllerTarget.GetComponent<Outline>().enabled = true;
                            else
                                controllerTarget.transform.GetChild(0).GetComponent<Outline>().enabled = true;
                        }
                        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + (i + 1) + "Button7")))
                            Play();
                    }
                }
                //for (int i = 0; i < Input.GetJoystickNames().Length; i++)
                //{
                //    if (Input.GetAxis("Vertical " + i) >= 0.8f)
                //    {
                //        switchTimer = switchInterval;
                //
                //        if (!dropdown)
                //        {
                //            currentElement++;
                //            if (currentElement >= cardElements.Length)
                //                currentElement = 0;
                //
                //            if (controllerTarget.GetComponent<Toggle>() == null)
                //                controllerTarget.GetComponent<Outline>().enabled = false;
                //            else
                //                controllerTarget.transform.GetChild(0).GetComponent<Outline>().enabled = false;
                //
                //            controllerTarget = cardElements[currentElement];
                //
                //            if (controllerTarget.GetComponent<Toggle>() == null)
                //                controllerTarget.GetComponent<Outline>().enabled = true;
                //            else
                //                controllerTarget.transform.GetChild(0).GetComponent<Outline>().enabled = true;
                //        }
                //        else
                //        {
                //            currentDropdownOption++;
                //            if (currentDropdownOption >= dropdownOptions.Count)
                //                currentDropdownOption = 0;
                //        }
                //    }
                //    else if (Input.GetAxis("Vertical " + i) <= -0.8f)
                //    {
                //        switchTimer = switchInterval;
                //
                //        if (!dropdown)
                //        {
                //            currentElement--;
                //            if (currentElement < 0)
                //                currentElement = cardElements.Length - 1;
                //
                //            if (controllerTarget.GetComponent<Toggle>() == null)
                //                controllerTarget.GetComponent<Outline>().enabled = false;
                //            else
                //                controllerTarget.transform.GetChild(0).GetComponent<Outline>().enabled = false;
                //
                //            controllerTarget = cardElements[currentElement];
                //
                //            if (controllerTarget.GetComponent<Toggle>() == null)
                //                controllerTarget.GetComponent<Outline>().enabled = true;
                //            else
                //                controllerTarget.transform.GetChild(0).GetComponent<Outline>().enabled = true;
                //        }
                //        else
                //        {
                //            dropdownOptions[currentDropdownOption].transform.GetChild(0).GetComponent<Outline>().enabled = false;
                //
                //            currentDropdownOption--;
                //            if (currentDropdownOption < 0)
                //                currentDropdownOption = dropdownOptions.Count - 1;
                //
                //            dropdownOptions[currentDropdownOption].transform.GetChild(0).GetComponent<Outline>().enabled = true;
                //        }
                //    }
                //
                //    if (Input.GetAxis("Horizontal " + i) >= 0.8f)
                //    {
                //        switchTimer = switchInterval;
                //
                //        if (controllerTarget.GetComponent<Toggle>() == null)
                //            controllerTarget.GetComponent<Outline>().enabled = false;
                //        else
                //            controllerTarget.transform.GetChild(0).GetComponent<Outline>().enabled = false;
                //
                //        currentCard++;
                //        if (currentCard >= playerCards.Length)
                //            currentCard = 0;
                //
                //        setCardElements();
                //        controllerTarget = cardElements[currentElement];
                //
                //        if (controllerTarget.GetComponent<Toggle>() == null)
                //            controllerTarget.GetComponent<Outline>().enabled = true;
                //        else
                //            controllerTarget.transform.GetChild(0).GetComponent<Outline>().enabled = true;
                //    }
                //    else if (Input.GetAxis("Horizontal " + i) <= -0.8f)
                //    {
                //        switchTimer = switchInterval;
                //
                //        if (controllerTarget.GetComponent<Toggle>() == null)
                //            controllerTarget.GetComponent<Outline>().enabled = false;
                //        else
                //            controllerTarget.transform.GetChild(0).GetComponent<Outline>().enabled = false;
                //
                //        currentCard--;
                //        if (currentCard < 0)
                //            currentCard = playerCards.Length - 1;
                //
                //        setCardElements();
                //        controllerTarget = cardElements[currentElement];
                //
                //        if (controllerTarget.GetComponent<Toggle>() == null)
                //            controllerTarget.GetComponent<Outline>().enabled = true;
                //        else
                //            controllerTarget.transform.GetChild(0).GetComponent<Outline>().enabled = true;
                //    }
                //}
            }
        }
    }
    //public void setCardElements()
    //{
    //    PlayerCard currentScript = playerCards[currentCard].GetComponent<PlayerCard>();
    //    if (currentScript.isPlaying)
    //    {
    //        if (currentScript.isController)
    //        {
    //            cardElements = new GameObject[]
    //            {
    //                levels[currentCard],
    //                currentScript.playing,
    //                currentScript.controller,
    //                currentScript.controllerDropdown,
    //                currentScript.jump,
    //                currentScript.dash,
    //                currentScript.special,
    //                currentScript.pause,
    //                playButton
    //            };
    //        }
    //        else
    //        {
    //            cardElements = new GameObject[]
    //            {
    //                levels[currentCard],
    //                currentScript.playing,
    //                currentScript.controller,
    //                currentScript.up,
    //                currentScript.down,
    //                currentScript.left,
    //                currentScript.right,
    //                currentScript.jump,
    //                currentScript.dash,
    //                currentScript.special,
    //                currentScript.pause,
    //                playButton
    //            };
    //        }
    //    }
    //    else
    //    {
    //        if (currentScript.isAI)
    //        {
    //            cardElements = new GameObject[]
    //            {
    //                levels[currentCard],
    //                currentScript.ai,
    //                playButton
    //            };
    //        }
    //        else
    //        {
    //            cardElements = new GameObject[]
    //            {
    //                levels[currentCard],
    //                currentScript.playing,
    //                currentScript.ai,
    //                playButton
    //            };
    //        }
    //    }

    //    if (currentElement >= cardElements.Length)
    //        currentElement = cardElements.Length - 1;
    //}

    public void Menu()
    {
        Time.timeScale = 1;
        if (controllerTarget != null && controllerTarget.GetComponent<Outline>() != null)
            controllerTarget.GetComponent<Outline>().enabled = false;
        menuScreen.SetActive(true);
        playerScreen.SetActive(false);
        creditsScreen.SetActive(false);
        controllerTarget = menuButtons[0];
        controllerTarget.GetComponent<Outline>().enabled = true;
        currentMenuButton = 0;
    }

    public void Credits()
    {
        menuScreen.SetActive(false);
        playerScreen.SetActive(false);
        creditsScreen.SetActive(true);
        controllerTarget.GetComponent<Outline>().enabled = false;
        controllerTarget = creditsScreen.transform.GetChild(6).gameObject;
        controllerTarget.GetComponent<Outline>().enabled = true;
    }

    public void PlayerSelect()
    {
        menuScreen.SetActive(false);
        playerScreen.SetActive(true);
        //setCardElements();
        if (controllerTarget.GetComponent<Toggle>() == null)
            controllerTarget.GetComponent<Outline>().enabled = true;
        else
            controllerTarget.transform.GetChild(0).GetComponent<Outline>().enabled = true;
    }

    public void SelectLevel(string level)
    {
        selectedLevel = level;
    }

    public void SetLevelSelected(int level)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].transform.GetChild(1).gameObject.SetActive(false);
        }
        levels[level].transform.GetChild(1).gameObject.SetActive(true);
    }

    public void Play()
    {
        GetComponent<GameSetup>().Play(selectedLevel);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
