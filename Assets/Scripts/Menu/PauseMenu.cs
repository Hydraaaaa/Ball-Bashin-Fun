using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum pauseState
{
    mainMenu,
    powerups,
    quitConfirm,
    levelEvents
}

public class PauseMenu : MonoBehaviour
{
    GameObject controllerTarget;
    pauseState state;
    public GameObject[] mainMenuItems;
    public GameObject[] powerupsItems;
    public GameObject[] levelEventsItems;
    public GameObject[] quitConfirmItems;
    int currentItem;

    bool[] switchedX;
    bool[] switchedY;

    public GameObject confirmQuit;
    public GameObject powerupsMenu;
    public GameObject powerupsInfo;
    public GameObject levelEventsMenu;
    public Sprite[] matArray;
    int count;

    void Start()
    {
        state = pauseState.mainMenu;
        count = 0;
        currentItem = 0;

        switchedX = new bool[Input.GetJoystickNames().Length];
        switchedY = new bool[Input.GetJoystickNames().Length];

        for (int i = 0; i < switchedX.Length; i++)
        {
            switchedX[i] = false;
        }
        for (int i = 0; i < switchedY.Length; i++)
        {
            switchedY[i] = false;
        }
        controllerTarget = mainMenuItems[currentItem];
        controllerTarget.GetComponent<Outline>().enabled = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            if (controllerTarget.GetComponent<Button>() != null)
                controllerTarget.GetComponent<Button>().onClick.Invoke();
            else if (controllerTarget.GetComponent<Toggle>() != null)
                controllerTarget.GetComponent<Toggle>().isOn = !controllerTarget.GetComponent<Toggle>().isOn;
        }
        if (state == pauseState.mainMenu)
        {
            for (int i = 0; i < Input.GetJoystickNames().Length; i++)
            {
                if (!switchedY[i])
                {
                    if (Input.GetAxis("Vertical " + i) >= 0.8f)
                    {
                        switchedY[i] = true;

                        currentItem++;
                        if (currentItem >= mainMenuItems.Length)
                            currentItem = 0;

                        if (controllerTarget.GetComponent<Toggle>() == null)
                            controllerTarget.GetComponent<Outline>().enabled = false;
                        else
                            controllerTarget.transform.GetChild(0).GetComponent<Outline>().enabled = false;

                        controllerTarget = mainMenuItems[currentItem];

                        if (controllerTarget.GetComponent<Toggle>() == null)
                            controllerTarget.GetComponent<Outline>().enabled = true;
                        else
                            controllerTarget.transform.GetChild(0).GetComponent<Outline>().enabled = true;
                    }
                    else if (Input.GetAxis("Vertical " + i) <= -0.8f)
                    {
                        switchedY[i] = true;

                        currentItem--;
                        if (currentItem < 0)
                            currentItem = mainMenuItems.Length - 1;

                        if (controllerTarget.GetComponent<Toggle>() == null)
                            controllerTarget.GetComponent<Outline>().enabled = false;
                        else
                            controllerTarget.transform.GetChild(0).GetComponent<Outline>().enabled = false;

                        controllerTarget = mainMenuItems[currentItem];

                        if (controllerTarget.GetComponent<Toggle>() == null)
                            controllerTarget.GetComponent<Outline>().enabled = true;
                        else
                            controllerTarget.transform.GetChild(0).GetComponent<Outline>().enabled = true;
                    }
                }
            }
        }
        else if (state == pauseState.powerups)
        {
            for (int i = 0; i < Input.GetJoystickNames().Length; i++)
            {
                if (!switchedX[i])
                {
                    if (Input.GetAxis("Horizontal " + i) >= 0.8f)
                    {
                        switchedX[i] = true;

                        currentItem++;
                        if (currentItem >= powerupsItems.Length)
                            currentItem = 0;

                        if (controllerTarget.GetComponent<Toggle>() == null)
                            controllerTarget.GetComponent<Outline>().enabled = false;
                        else
                            controllerTarget.transform.GetChild(0).GetComponent<Outline>().enabled = false;

                        controllerTarget = powerupsItems[currentItem];

                        if (controllerTarget.GetComponent<Toggle>() == null)
                            controllerTarget.GetComponent<Outline>().enabled = true;
                        else
                            controllerTarget.transform.GetChild(0).GetComponent<Outline>().enabled = true;
                    }
                    else if (Input.GetAxis("Horizontal " + i) <= -0.8f)
                    {
                        switchedX[i] = true;

                        currentItem--;
                        if (currentItem < 0)
                            currentItem = powerupsItems.Length - 1;

                        if (controllerTarget.GetComponent<Toggle>() == null)
                            controllerTarget.GetComponent<Outline>().enabled = false;
                        else
                            controllerTarget.transform.GetChild(0).GetComponent<Outline>().enabled = false;

                        controllerTarget = powerupsItems[currentItem];

                        if (controllerTarget.GetComponent<Toggle>() == null)
                            controllerTarget.GetComponent<Outline>().enabled = true;
                        else
                            controllerTarget.transform.GetChild(0).GetComponent<Outline>().enabled = true;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < Input.GetJoystickNames().Length; i++)
            {
                if (!switchedX[i])
                {
                    if (Input.GetAxis("Horizontal " + i) >= 0.8f)
                    {
                        switchedX[i] = true;

                        currentItem++;
                        if (currentItem >= quitConfirmItems.Length)
                            currentItem = 0;

                        if (controllerTarget.GetComponent<Toggle>() == null)
                            controllerTarget.GetComponent<Outline>().enabled = false;
                        else
                            controllerTarget.transform.GetChild(0).GetComponent<Outline>().enabled = false;

                        controllerTarget = quitConfirmItems[currentItem];

                        if (controllerTarget.GetComponent<Toggle>() == null)
                            controllerTarget.GetComponent<Outline>().enabled = true;
                        else
                            controllerTarget.transform.GetChild(0).GetComponent<Outline>().enabled = true;
                    }
                    else if (Input.GetAxis("Horizontal " + i) <= -0.8f)
                    {
                        switchedX[i] = true;

                        currentItem--;
                        if (currentItem < 0)
                            currentItem = quitConfirmItems.Length - 1;

                        if (controllerTarget.GetComponent<Toggle>() == null)
                            controllerTarget.GetComponent<Outline>().enabled = false;
                        else
                            controllerTarget.transform.GetChild(0).GetComponent<Outline>().enabled = false;

                        controllerTarget = quitConfirmItems[currentItem];

                        if (controllerTarget.GetComponent<Toggle>() == null)
                            controllerTarget.GetComponent<Outline>().enabled = true;
                        else
                            controllerTarget.transform.GetChild(0).GetComponent<Outline>().enabled = true;
                    }
                }
            }
        }
        for (int i = 0; i < Input.GetJoystickNames().Length; i++)
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal " + i)) < 0.8f)
            {
                switchedX[i] = false;
            }
            if (Mathf.Abs(Input.GetAxis("Vertical " + i)) < 0.8f)
            {
                switchedY[i] = false;
            }
        }
    }

    public void returnToMenu()
    {
        confirmQuit.SetActive(true);
        controllerTarget.GetComponent<Outline>().enabled = false;

        state = pauseState.quitConfirm;
        currentItem = 0;
        controllerTarget = quitConfirmItems[currentItem];

        controllerTarget.GetComponent<Outline>().enabled = true;
    }

    public void quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void dontQuit()
    {
        confirmQuit.SetActive(false);

        state = pauseState.mainMenu;
        currentItem = 0;
        controllerTarget = mainMenuItems[currentItem];

        controllerTarget.GetComponent<Outline>().enabled = true;
    }

    public void levelEvents()
    {
        state = pauseState.levelEvents;
        currentItem = 0;
        controllerTarget = levelEventsItems[currentItem];

        controllerTarget.GetComponent<Outline>().enabled = true;
        levelEventsMenu.SetActive(true);
    }

    public void powerups()
    {
        state = pauseState.powerups;
        currentItem = 0;
        controllerTarget = powerupsItems[currentItem];

        controllerTarget.GetComponent<Outline>().enabled = true;
        powerupsMenu.SetActive(true);
    }

    public void exitScreen()
    {
        controllerTarget.GetComponent<Outline>().enabled = false;
        state = pauseState.mainMenu;
        currentItem = 0;
        controllerTarget = mainMenuItems[currentItem];

        controllerTarget.GetComponent<Outline>().enabled = true;
        powerupsMenu.SetActive(false);
        levelEventsMenu.SetActive(false);
    }

    public void fwdPress()
    {
        count++;
        if (count > matArray.Length - 1)
        {
            count = 0;
        }
        powerupsInfo.transform.GetComponent<Image>().sprite = matArray[count];
    }

    public void bckPress()
    {
        count--;
        if (count < 0)
        {
            count = matArray.Length - 1;
        }
        powerupsInfo.transform.GetComponent<Image>().sprite = matArray[count];
    }
}
