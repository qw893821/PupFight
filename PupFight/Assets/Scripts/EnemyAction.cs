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

    public GameObject testGO;//all possible "MoveRangeGO" in this area. 
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
    int a = 4;//this is not a good idea to set a value here, but have not idea how to deal with it.

    int range;//attack range
    public GameObject[] enemyRangeHLs;
    public GameObject enemyRangeHL;
    GameObject[] players;

    Ray enemyRay;
    RaycastHit hit;

    private void Awake()
    {
        moveRangeGO = new GameObject[4];
    }
    // Use this for initialization
    void Start () {
        enemyStatus = ActionStatus.isDone;
        players = GameObject.FindGameObjectsWithTag("Player");
        switch (rangeType)//this is attack range
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
        
    }
	

	// Update is called once per frame
	void Update () {
        GetCurrentPos();
        TargetSearch();
        MoveGridStatusChange();
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
        switch (moveRange) {
            case 2:
                for (int i = 0; i < a; i++)
                {
                    moveRangeGO[i] = GameObject.Find("Range2Area" + i);
                }
                break;
            case 3:
                for (int i = 0; i < a; i++)
                {
                    moveRangeGO[i] = GameObject.Find("Range3Area" + i);

                }
                break;
        }
    }

    //maybe, the problem is the ray could not hit the collider in this go. so, find a solution to solve issue.
    //one track, change to instanicate of "moverangego", do not set it be the child of this gameobject.
    //2, maybe use the "enemyactionmanager" to cast the ray. There is one manager, this could do something.
    public void FindTestPos()
    {
        foreach(GameObject mRGO in moveRangeGO)
        {
            mRGO.transform.position = transform.position;
        }
        GameObject go;
        int layerMask = 1 << 10;
        enemyRay=new Ray(transform.position + new Vector3(0, 0.5f, 0), target.transform.position-transform.position);
        if(Physics.Raycast(enemyRay,out hit,10f,layerMask))
        {
            //get all position infor of grid which this ray hit, create a pos array for pos test.
            testGO = hit.collider.transform.parent.gameObject;
        }
        else { go = null; }

        int tempCount=0;
        testPoss = new Vector3[testGO.transform.childCount - 1];
        foreach(Transform child in testGO.transform)
        {
            if (child.gameObject.tag == "MoveArea")
            {
                testPoss[tempCount] = child.transform.position;
                tempCount++;
            }
        }
        tempCount = 0;
    }


   void GetCurrentPos()
    {
        if (enemyStatus == ActionStatus.isDone&&currentPos!=transform.position)//when enemy finished its action, store current position. Excute when "currentPos" is different from enemy's current position
        {
            currentPos = transform.position;
        }
    }

   void MoveGridStatusChange()
    {
        if (EnemyActionManager.eAmanager.EnemyFinished())
        {
            foreach(GameObject go in moveRangeGO)
            {
                go.SetActive(false);
            }
        }
        else {
            foreach (GameObject go in moveRangeGO)
            {
                go.SetActive(true);
            }
        }
    } 
}
