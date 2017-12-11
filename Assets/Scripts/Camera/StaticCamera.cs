using UnityEngine;
using System.Collections;

public class StaticCamera : MonoBehaviour
{
    public ObjectManager library;
    public GameObject[] players;

    Vector2 origin;
    public float baseDistance;
    public float distanceMultiplier;

    bool setStartPos;

    void Awake()
    {
        library = null;
        setStartPos = true;
    }

    void Start()
    {
        origin = Vector2.zero;
    }

    void Update()
    {
        if (library != null)
        {
            origin = Vector2.zero;
            for (int i = 0; i < library.players.Count; i++)
            {
                origin += new Vector2(library.players[i].transform.position.x, library.players[i].transform.position.z);
            }
            origin /= library.players.Count;

            float distance = 0;
            for (int i = 0; i < library.players.Count; i++)
            {
                if ((Vector2.Distance(origin, new Vector2(library.players[i].transform.position.x, library.players[i].transform.position.z))) > distance)
                    distance = Vector2.Distance(origin, new Vector2(library.players[i].transform.position.x, library.players[i].transform.position.z));
            }
            if (setStartPos)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(origin.x, 0, origin.y) - baseDistance * transform.forward - distance * distanceMultiplier * transform.forward, 10f);
                setStartPos = false;
            }
            else
                transform.position = Vector3.Lerp(transform.position, new Vector3(origin.x, 0, origin.y) - baseDistance * transform.forward - distance * distanceMultiplier * transform.forward, 0.05f);
        }
    }
}
