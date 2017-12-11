using UnityEngine;
using System.Collections;

public class Slime : MonoBehaviour
{
    public GameObject owner;
    public float life;
    public float strength;
    public float size;
    float alpha;
    float currentScale;

    void Start ()
    {
        alpha = GetComponent<Renderer>().material.color.a;
        currentScale = 0;
        GetComponent<Renderer>().material = Resources.Load("Materials/Powerups/SlimeSplash" + owner.GetComponent<Ball>().num, typeof(Material)) as Material;
    }
	
	void Update ()
    {
        life -= Time.deltaTime;
        if (currentScale < size)
            currentScale += Time.deltaTime;

        if (transform.localScale.x < size || transform.localScale.y < size)
            transform.localScale = new Vector3(currentScale, currentScale, currentScale);

        if (life <= 1)
        {
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, alpha * life);
        }
        if (life <= 0)
            Destroy(gameObject);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flower"))
        {
            transform.parent = other.transform;
            transform.rotation = Quaternion.Euler(90, 0, 0);
        }
        if (other.CompareTag("Player") && other.gameObject != owner)
        {
            other.GetComponent<Rigidbody>().velocity /= 1 + strength / other.transform.localScale.magnitude;
        }
    }
}
