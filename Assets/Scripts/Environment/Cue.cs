using UnityEngine;
using System.Collections;

public class Cue : MonoBehaviour
{
    [HideInInspector]
    public GameObject targetPlayer;
    [HideInInspector]
    public Vector3 targetPos;

    public float distance;
    public float angleY;
    public float aimDuration;
    public float hitVelocity;

    public float postShotLife;

    public float playerDisableTime;

    bool shotsFired;

	void Start ()
    {
        shotsFired = false;
	}
	
	void Update ()
    {
        if (aimDuration > 0)
        {
            Vector2 direction2D = (new Vector2(targetPos.x, targetPos.z) - new Vector2(targetPlayer.transform.position.x, targetPlayer.transform.position.z)).normalized;
            Vector3 direction = new Vector3(direction2D.x, -angleY, direction2D.y).normalized;
            transform.position = targetPlayer.transform.position - direction * distance;
            transform.LookAt(targetPlayer.transform);

            aimDuration -= Time.deltaTime;
        }
        else
        {
            postShotLife -= Time.deltaTime;

            if (postShotLife <= 0)
                Destroy(gameObject);

            if (!shotsFired)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.velocity = transform.forward * hitVelocity;
                shotsFired = true;
            }
        }
	}

    void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player") || other.gameObject == targetPlayer)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            if (other.gameObject == targetPlayer && targetPlayer.GetComponent<PlayerMovement>() != null)
                targetPlayer.GetComponent<PlayerMovement>().disableDuration = playerDisableTime;
        }
    }
}
