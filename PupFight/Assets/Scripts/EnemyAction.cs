using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour {

    /*enemy status could be "isWaiting" when it is player't turn
     *enemy status "isActing" when enemy is acting. Only one enemy is actin one time
     *enemy status "isDone" the enemy when enemy finished acting in enemy turn. All enemies finished their turn then player's turn
     */
    public ActionStatus enemyStatus;

    public GameObject target;

    GameObject[] players;
    // Use this for initialization
    void Start () {
        enemyStatus = ActionStatus.isDone;
        players = GameObject.FindGameObjectsWithTag("Player");
	}
	

	// Update is called once per frame
	void Update () {
		
	}

    void TargetSearch()
    {
        GameObject tempGO;
        float nearDist;//nearest player character
        float tempDist;//temp distance data
        tempGO = players[0];
        nearDist = Vector3.Distance(transform.position, players[0].transform.position);
        for (int i = 1; i < players.Length; i++)
        {
                    tempDist = Vector3.Distance(transform.position, players[i].transform.position);
                    if (nearDist > tempDist)
                    {
                        tempGO = players[i];
                nearDist = tempDist;
                    }
        }
        target = tempGO;
    }

    /*bool TargetInRange()//range include two value. walking range + attack range
    {

        return true;
    }*/

    bool TargetInRange(int i, GameObject go)
    {
            Vector3 offset;
            offset = transform.position - go.transform.position;

            if ((Mathf.Abs(offset.x) / 1 + Mathf.Abs(offset.z) / 1) <= i)
            {
                return true;
            }
            else return false;      
    }

    public void Movement()
    {
        
    }
}
