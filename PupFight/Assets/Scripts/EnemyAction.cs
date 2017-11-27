using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour {

    /*enemy status could be "isWaiting" when it is player't turn
     *enemy status "isActing" when enemy is acting. Only one enemy is actin one time
     *enemy status "isDone" the enemy when enemy finished acting in enemy turn. All enemies finished their turn then player's turn
     */
    public ActionStatus enemyStatus;

    Vector3 currentPos;

    public Vector3[] testPoss;//date of all the possible position enemy could go

    public GameObject target;//target player character
    public GameObject enemyTargetGrid;//target grid,enemy will move

    //stroe current pos data of the gird which enemy character is in.
    public GameObject currentGrid;
    //attack range
    public RangeType rangeType;


    public int moveRange;//enemey move range;
    public GameObject [] moveRangeGO;
    Vector3 targetPos;//the target position of enemy, used for pos test

    int range;//attack range
    public GameObject[] enemyRangeHLs;
    public GameObject enemyRangeHL;
    GameObject[] players;

    Ray enemyRay;
    RaycastHit hit;
    // Use this for initialization
    void Start () {
        print(WalkRangeManager.walkRangeInstance.moveArea1);
        enemyStatus = ActionStatus.isDone;
        players = GameObject.FindGameObjectsWithTag("Player");
        switch (rangeType)
        {
            case RangeType.melee:
                range = 1;
                enemyRangeHL = enemyRangeHLs[0];
                break;
            case RangeType.shoot:
                range = 2;
                enemyRangeHL = enemyRangeHLs[1];
                break;
        }
        MoveRangePick();
        foreach(GameObject go in moveRangeGO)
        {
            Instantiate(go,transform);
        }

    }
	

	// Update is called once per frame
	void Update () {
        GetCurrentPos();
        TargetSearch();
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

    public void SetWaiting()
    {
        enemyStatus = ActionStatus.isWaiting;
    }

    void MoveRangePick()
    {
        switch (moveRange)
        {
            case 2:
                moveRangeGO = WalkRangeManager.walkRangeInstance.moveArea1;
                break;
            case 3:
                moveRangeGO = WalkRangeManager.walkRangeInstance.moveArea2;
                break;
        }

    }

    //maybe, the problem is the ray could not hit the collider in this go. so, find a solution to solve issue.
    //one track, change to instanicate of "moverangego", do not set it be the child of this gameobject.
    //2, maybe use the "enemyactionmanager" to cast the ray. There is one manager, this could do something.
    /*public void FindTestPos()
    {
        GameObject go;
        int layerMask = 1 << 10;
        enemyRay=new Ray(transform.position + new Vector3(0, 0.5f, 0), target.transform.position-transform.position);
        print(enemyRay);
        if(Physics.Raycast(enemyRay,out hit,Mathf.Infinity, layerMask))
        {
            print("anything");
        }
        else { go = null; }
        

    }*/


   void GetCurrentPos()
    {
        if (enemyStatus == ActionStatus.isDone&&currentPos!=transform.position)//when enemy finished its action, store current position. Excute when "currentPos" is different from enemy's current position
        {
            currentPos = transform.position;
        }
    }
}
