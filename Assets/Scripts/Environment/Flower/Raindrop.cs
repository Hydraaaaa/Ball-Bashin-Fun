using UnityEngine;
using System.Collections;

public class Raindrop : MonoBehaviour {

    // Use this for initialization

    GameObject droplet;
    public float timeLeft = 5.0f;
    public float time = 0;
    public Raindrop()
    {
        GameObject Raindroplet = GameObject.CreatePrimitive(PrimitiveType.Quad);
        Raindroplet.AddComponent<Rigidbody>();
        //Raindroplet.GetComponent<Renderer>().material = objColour;
        Raindroplet.GetComponent<Collider>().enabled = false;
        //Raindroplet.GetComponent<Renderer>().tag = "RainDrop";
        Raindroplet.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        Raindroplet.name = "Raindrop";
        droplet = Raindroplet;
        //Update();
    }

    public Raindrop(float rainDropSize)
    {
        GameObject Raindroplet = GameObject.CreatePrimitive(PrimitiveType.Quad);
        Raindroplet.AddComponent<Rigidbody>();
        //Raindroplet.GetComponent<Renderer>().material = objColour;
        Raindroplet.GetComponent<Collider>().enabled = false;
        //Raindroplet.GetComponent<Renderer>().tag = "RainDrop";
        Raindroplet.transform.localScale = new Vector3(rainDropSize, rainDropSize, rainDropSize);
        Raindroplet.name = "Raindrop";
        droplet = Raindroplet;
        //Update();
    }
    ~Raindrop()
    {
    }

    //public void rainFnct ()
    //{
    //    //if (droplet.transform.position.y < -40)
    //    //{
    //    //    dropDestroy();

    //    //}
    //    //Debug.Log(time);
    //    //while (time < timeLeft)
    //    {
    //        time += Time.deltaTime;
    //        if (time >= timeLeft)
    //        {
    //            //Destroy(this);
    //            Debug.Log("Meme");
    //            dropDestroy();
    //            time = 0;
    //            //m_drops--;
    //        }
    //    }
    //}

    public GameObject GetRainDrop()
    {
        return droplet;
    }

    public void SetRainDropColour(Material objColour)
    {
        droplet.GetComponent<Renderer>().material = objColour;
    }

    public void SetLocation(Vector3 Loc)
    {
        droplet.transform.position = Loc;
    }

    public void dropDestroy()
    {
        if (droplet != null)
        {
            Destroy(droplet);
        }
    }
}
