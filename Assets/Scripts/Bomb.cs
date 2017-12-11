using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bomb : MonoBehaviour
{
    public GameObject currentPlayer;
    public float lifeTime;
    float currentTime;
    public float forceMultiplier;

    public GameObject particle;

	void Start ()
    {
        GetComponent<AudioSource>().Play();
        currentTime = lifeTime;
	}
	
	void Update ()
    {
        currentTime -= Time.deltaTime;
        transform.position = currentPlayer.transform.position;
        transform.LookAt(transform.position + Camera.main.transform.rotation * -Vector3.right, Camera.main.transform.rotation * Vector3.up);
        transform.Rotate(0, 90, 0);

        // Time to Explode
        if (currentTime <= 0)
        {
            Vector3 force = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0, 1f), Random.Range(-0.5f, 0.5f));
            force.Normalize();
            force *= forceMultiplier;
            currentPlayer.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);

            GameObject explosion = Instantiate(particle, transform.position, Quaternion.identity) as GameObject;
            explosion.GetComponent<AudioSource>().Play();

            Destroy(gameObject);
        }

        transform.GetChild(0).GetChild(1).GetComponent<Image>().fillAmount = currentTime / lifeTime;
	}

    void OnTriggerEnter(Collider other)
    {
        // Switch Player
        if (other.CompareTag("Player"))
        {
            currentPlayer = other.gameObject;
        }
    }
}
