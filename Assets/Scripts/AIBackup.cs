using UnityEngine;

public class AIBackup : MonoBehaviour
{
    public ObjectManager objScript;
    Rigidbody rb;

    public float speed;
    public float maxSpeed;
    public float airborneSpeedDivider;

    GameObject closestTarget;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.GetChild(0).eulerAngles = Vector3.zero;
        closestTarget = null;
        float targetDistance = 999;
        for (int i = objScript.players.Count - 1; i >= 0; i--)
        {
            if (Vector3.Distance(transform.position, objScript.players[i].transform.position) < targetDistance && objScript.players[i].GetComponent<PlayerMovement>() != null)
            {
                targetDistance = Vector3.Distance(transform.position, objScript.players[i].transform.position);
                closestTarget = objScript.players[i];
            }
        }
        if (closestTarget == null)
        {
            for (int i = objScript.players.Count - 1; i >= 0; i--)
            {
                if (Vector3.Distance(transform.position, objScript.players[i].transform.position) < targetDistance && objScript.players[i] != gameObject)
                {
                    targetDistance = Vector3.Distance(transform.position, objScript.players[i].transform.position);
                    closestTarget = objScript.players[i];
                }
            }
        }

        //if (GetComponent<Ball>().currentPowerup == powerups.none)
        //{
        //    for (int i = objScript.powerups.Count - 1; i >= 0; i--)
        //    {
        //        if (Vector3.Distance(transform.position, objScript.powerups[i].transform.position) < targetDistance)
        //        {
        //            targetDistance = Vector3.Distance(transform.position, objScript.powerups[i].transform.position);
        //            closestTarget = objScript.powerups[i];
        //        }
        //    }
        //}

        if (!objScript.paused && closestTarget != null)
        {
            if (GetComponent<Ball>().grounded)
                rb.AddForce(Vector3.Normalize(closestTarget.transform.position - transform.position) * speed, ForceMode.Acceleration);
            else
                rb.AddForce(Vector3.Normalize(closestTarget.transform.position - transform.position) * speed / airborneSpeedDivider, ForceMode.Acceleration);
        }

        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.z, -maxSpeed, maxSpeed));
    }
}