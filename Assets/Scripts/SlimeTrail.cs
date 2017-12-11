using UnityEngine;
using System.Collections;

public class SlimeTrail : MonoBehaviour
{
    public int slimeCount;
    int currentSlimeCount;

    public GameObject slimeObject;
    GameObject prevSlime;

    public float slimeDistance;

	void Start ()
    {
        currentSlimeCount = slimeCount;
    }
	
	void Update ()
    {
        if (currentSlimeCount <= 0)
        {
            GetComponent<Ball>().currentPowerup = powerups.none;
            transform.GetComponent<Renderer>().material = GetComponent<Ball>().originalMaterial;
            enabled = false;
            GetComponent<PlayerMovement>().slimeToggle = false;
        }

        if (GetComponent<Ball>().grounded && prevSlime == null || GetComponent<Ball>().grounded && Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(prevSlime.transform.position.x, prevSlime.transform.position.z)) > slimeDistance)
        {
            prevSlime = Instantiate(slimeObject, new Vector3(transform.position.x, transform.position.y - 0.45f, transform.position.z), Quaternion.Euler(90, 0, 0)) as GameObject;
            //Physics.IgnoreCollision(GetComponent<Collider>(), prevSlime.GetComponent<Collider>());
            prevSlime.GetComponent<Slime>().owner = gameObject;
            prevSlime.transform.localScale = new Vector3(slimeObject.transform.localScale.x / 10, slimeObject.transform.localScale.y, slimeObject.transform.localScale.z / 10);
            currentSlimeCount--;
        }
    }

    public void resetSlime()
    {
        currentSlimeCount = slimeCount;
    }
}
