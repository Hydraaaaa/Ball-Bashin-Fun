using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;

[System.Serializable]
public struct cueAmount
{
    public float time;
    public int amount;
};

public class PoolCues : MonoBehaviour
{
    public GameObject poolCue;

    public Vector3[] holes = new Vector3[4];
    public cueAmount[] cueAmounts;

    public float cooldownMin; // Time between pool cue spawns
    public float cooldownMax; //
    float currentCooldown;

	void Start ()
    {
        currentCooldown = Random.Range(cooldownMin, cooldownMax);
	}
	
	void Update ()
    {
        currentCooldown -= Time.deltaTime;
        
        if (currentCooldown <= 0)
        {
            cueAmount currentCueAmount = cueAmounts[0];
            foreach (cueAmount amount in cueAmounts)
            {
                if (GetComponent<ObjectManager>().timeElapsed < amount.time)
                    continue;

                if (GetComponent<ObjectManager>().timeElapsed - amount.time < GetComponent<ObjectManager>().timeElapsed - currentCueAmount.time)
                    currentCueAmount = amount;
            }

            bool[] playersChosen = new bool[GetComponent<ObjectManager>().players.Count];

            for (int i = 0; i < playersChosen.Length; i++)
            {
                playersChosen[i] = false;
            }

            for (int i = 0; i < currentCueAmount.amount && i < GetComponent<ObjectManager>().players.Count; i++)
            {
                int rand;
                do
                {
                    rand = Random.Range(0, GetComponent<ObjectManager>().players.Count);
                } while (playersChosen[rand] != false);
                playersChosen[rand] = true;

                GameObject targetPlayer = GetComponent<ObjectManager>().players[rand];

                Vector3 currentHole = Vector3.zero;
                float currentDistance = 99999;
                foreach (Vector3 hole in holes)
                {
                    if (Vector3.Distance(targetPlayer.transform.position, hole) < currentDistance)
                    {
                        currentDistance = Vector3.Distance(targetPlayer.transform.position, hole);
                        currentHole = hole;
                    }
                }
                GameObject cue = Instantiate(poolCue);
                cue.GetComponent<Cue>().targetPlayer = targetPlayer;
                cue.GetComponent<Cue>().targetPos = currentHole;

                currentCooldown = Random.Range(cooldownMin, cooldownMax);
            }
        }
	}
}
