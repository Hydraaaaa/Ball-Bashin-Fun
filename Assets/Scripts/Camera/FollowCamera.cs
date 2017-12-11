using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{
    public GameObject target;

    public float followSpeed;
    public float heightSpeed;
    public float minimumDistance;
    public float maximumDistance;
    public float minimumHeight;
    public float maximumHeight;
    public float turnSpeed;

    public KeyCode turnLeft;
    public KeyCode turnRight;

    void Update()
    {
        if (Input.GetKey(turnLeft))
            transform.position -= transform.right * turnSpeed / 10;
        if (Input.GetKey(turnRight))
            transform.position += transform.right * turnSpeed / 10;

        transform.LookAt(target.transform.position);
        if (Vector3.Distance(transform.position, target.transform.position) < minimumDistance)
            transform.position = Vector3.Lerp(transform.position, transform.position - transform.forward, (minimumDistance - Vector3.Distance(transform.position, target.transform.position)) * followSpeed / 1000);
        if (Vector3.Distance(transform.position, target.transform.position) > maximumDistance)
            transform.position = Vector3.Lerp(transform.position, target.transform.position, (Vector3.Distance(transform.position, target.transform.position) - maximumDistance) * followSpeed / 1000);

        if (transform.position.y - target.transform.position.y < minimumHeight)
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), (target.transform.position.y + minimumHeight - transform.position.y) * heightSpeed / 100);
        if (transform.position.y - target.transform.position.y > maximumHeight)
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), transform.position.y - (target.transform.position.y + maximumHeight) * heightSpeed / 100);
    }
}
