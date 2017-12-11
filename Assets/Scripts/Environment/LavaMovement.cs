using UnityEngine;
using System.Collections;

public class LavaMovement : MonoBehaviour
{
    public Vector2 speed;
    Vector2 offset;

	void Start ()
    {
        offset = Vector2.zero;
	}
	
	void Update ()
    {
        offset += Time.deltaTime * speed;
        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", offset);
	}
}
