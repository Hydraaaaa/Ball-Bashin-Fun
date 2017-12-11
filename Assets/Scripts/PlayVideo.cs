using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayVideo : MonoBehaviour
{

    public MovieTexture movie;
    RawImage rawImageComp;
    public GameObject CamObj;
    public GameObject UIObj;
    public GameObject pressAnyKey;

    void Start()
    {
        rawImageComp = GetComponent<RawImage>();
        PlayClip();
    }

    void Update()
    {
        Time.timeScale = 0;
        if (!movie.isPlaying || Input.anyKey)
        {
            CamObj.GetComponent<LevelStartDelay>().enabled = true;
            CamObj.GetComponent<LevelStartDelay>().UIObj.SetActive(true);
            UIObj.SetActive(false);
            pressAnyKey.SetActive(false);
        }
    }

    void PlayClip()
    {
        rawImageComp.texture = movie;
        movie.Play();
    }
}
