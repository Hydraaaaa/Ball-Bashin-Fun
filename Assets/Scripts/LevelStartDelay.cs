using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelStartDelay : MonoBehaviour
{
    public float DelayTime;
    public float delay;

    public Sprite[] images;
    public GameObject UIObj;
    // Use this for initialization
    void Start()
    {
        delay = DelayTime;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 0.1f;
        UIObj.GetComponent<Image>().enabled = true;
        delay -= (Time.deltaTime * 10);
        //Time.timeScale = 0.001f;
        //UIObj.GetComponent<Text>().text = "" + ((int)delay + 1);
        UIObj.GetComponent<Image>().sprite = images[(int)delay];
        if (delay <= 0)
        {
            UIObj.GetComponent<Image>().enabled = false;
            delay = DelayTime;
            GetComponent<LevelStartDelay>().enabled = false;
            Time.timeScale = 1;
        }
    }
}

