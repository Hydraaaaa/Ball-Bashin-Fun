using UnityEngine;
using System.Collections;


//Callum's Material Change Script

public class ColourChange : MonoBehaviour {

    public enum ColourCode
    {
        None,
        Blue,
        Red,
        Yellow,
        Black,
        Silver,
        White,
        Green,
        Purple
    }
    public GameObject playerObj;
    public ColourCode colChange;

    //public int ticks;
    //public int maxticks;

    public KeyCode ColourChangeBlue;
    public KeyCode ColourChangeRed;
    public KeyCode ColourChangeYellow;
    public KeyCode ColourChangeBlack;

    public KeyCode ColourChangeBack;

    //public Material LastColour;
    public Material ColourBlue;
    public Material ColourRed;
    public Material ColourYellow;
    public Material ColourBlack;
    public Material ColourSilver;
    public Material ColourWhite;
    public Material ColourGreen;
    public Material ColourPurple;

    public Material defaultColour;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        #region TriggerColourChange
        //---EVENT---\\
        if (Input.GetKey(ColourChangeBlue))
        {
            colChange = ColourCode.Blue;
        }
        else if (Input.GetKey(ColourChangeRed))
        {
            colChange = ColourCode.Red;
        }
        else if (Input.GetKey(ColourChangeYellow))
        {
            colChange = ColourCode.Yellow;
        }
        else if (Input.GetKey(ColourChangeBlack))
        {
            colChange = ColourCode.Black;
        }
        else if (Input.GetKey(ColourChangeBack))
        {
            colChange = ColourCode.None;
            //ticks = 0;
        }
        //----END----\\
        #endregion
        #region ColourChanges
        switch (colChange) {
            case ColourCode.Blue:
                playerObj.GetComponent<Renderer>().material = ColourBlue;
                //LastColour = ColourBlue;
                //ticks = maxticks + 1;
                break;
            case ColourCode.Red:
                playerObj.GetComponent<Renderer>().material = ColourRed;
                //LastColour = ColourRed;
                //ticks = maxticks + 1;
                break;
            case ColourCode.Yellow:
                playerObj.GetComponent<Renderer>().material = ColourYellow;
                //LastColour = ColourYellow;
                //ticks = maxticks + 1;
                break;
            case ColourCode.Black:
                playerObj.GetComponent<Renderer>().material = ColourBlack;
                //LastColour = ColourBlack;
                //ticks = maxticks + 1;
                break;
            case ColourCode.Silver:
                playerObj.GetComponent<Renderer>().material = ColourSilver;
                break;
            case ColourCode.White:
                playerObj.GetComponent<Renderer>().material = ColourWhite;
                break;
            case ColourCode.Green:
                playerObj.GetComponent<Renderer>().material = ColourGreen;
                break;
            case ColourCode.Purple:
                playerObj.GetComponent<Renderer>().material = ColourPurple;
                break;
            default:
                //if (ticks <= maxticks)
                //{
                //if (playerObj.GetComponent<Renderer>().material != defaultColour)
                //{
                //playerObj.GetComponent<Renderer>().material = defaultColour;
                //ticks++;
                //}
                //else
                //{
                //playerObj.GetComponent<Renderer>().material = LastColour;
                //ticks++;
                //}
                //}
                playerObj.GetComponent<Renderer>().material = defaultColour;
                break;
        }
        #endregion
    }
}
