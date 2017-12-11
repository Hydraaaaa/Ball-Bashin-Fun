using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndlessVideo : MonoBehaviour
{
    public MovieTexture movie;
    RawImage rawImageComp;
    // Use this for initialization
    void Start()
    {
        rawImageComp = GetComponent<RawImage>();
        PlayClip();
    }

    // Update is called once per frame

    void PlayClip()
    {
        rawImageComp.texture = movie;
        movie.loop = true;
        movie.Play();
    }
}
