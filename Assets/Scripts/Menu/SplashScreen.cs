using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SplashScreen : MonoBehaviour
{
    public float SplashscreenLength;
    public GameObject UIScreen;
    public GameObject BackScreen;
    float timeDelay;
    static bool firstTime = true;
    // Use this for initialization
    void Start()
    {
        if (firstTime)
        {
            timeDelay = SplashscreenLength;
            UIScreen.SetActive(true);
            BackScreen.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeDelay -= Time.deltaTime;
        UIScreen.GetComponent<Image>().color = new Vector4(UIScreen.GetComponent<Image>().color.r, UIScreen.GetComponent<Image>().color.g, UIScreen.GetComponent<Image>().color.b, UIScreen.GetComponent<Image>().color.a - (Time.deltaTime / SplashscreenLength));
        if (timeDelay <= 0 || Input.anyKey)
        {
            UIScreen.SetActive(false);
            BackScreen.SetActive(false);
            firstTime = false;
        }
    }
}
