using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ActionStatus{
    isWaiting,
    isMoving,
    moved,
    isActing,
    isDone
}

public enum RangeType
{
    melee,
    shoot
}

public class PlayerAction : MonoBehaviour {

    //player move speed;
    float speed;
    
    //pup attack power
    public float attackPow;

    //MovingStatus
    public ActionStatus aStatus;

    //range GO
    public GameObject[] rangeGOs;

    //array of attack range
    public GameObject[] rangeHLs;
    public GameObject rangeHL;

    public RangeType range;

    //camera ray, shot ray to get the grid where the mouse is.
    Ray camRay;
    RaycastHit hit;

    //child component, must get before it is set inactive.
    Visibility rangeVB;
    // Use this for initialization
    
    void Start()
    {


        this.gameObject.name = this.gameObject.name.ToString();
        speed = 5f;
        attackPow = 5f;
        InstantiateRange();//this function instantate rangeHL GO
        Instantiate(rangeHL, transform);
        FindRange();
        InActiveRange();
    }

    private void Update()
    {

    }
    // Update is called once per frame
    void LateUpdate() {
       
        MovingCheck();
        

    }



    void MovePlayer()
    {
        Vector3 target;
        float offset;
        offset = transform.position.y - PlayerActionManager.instance.targetGrid.transform.position.y;
        target = new Vector3(PlayerActionManager.instance.targetGrid.transform.position.x, PlayerActionManager.instance.targetGrid.transform.position.y + offset, PlayerActionManager.instance.targetGrid.transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position == target)
        {
            aStatus = ActionStatus.moved;
        }

    }

    public void ChangeStatus()//change the status to let player move
    {
        aStatus = ActionStatus.isMoving;
    }


    public void Attacked()
    {
        aStatus = ActionStatus.isDone;
        InActiveRange();
    }
    

    public void MovingCheck()
    {
        switch (aStatus)
        {
            case ActionStatus.isMoving:
                MovePlayer();
                break;
        }
    }

    public bool FinishAction()
    {
        if (aStatus == ActionStatus.isDone)
        {
            return true;
        }
        else return false;
    }


    void InstantiateRange()
    {
        switch (range)
        {
            case RangeType.melee:
                rangeHL=rangeHLs[0];
                break;
            case RangeType.shoot:
                rangeHL=rangeHLs[1];
                break;
        }
        
    }

    public void FindRange()
    {
        rangeVB = transform.GetComponentInChildren<Visibility>();
    }

    public void ActiveRange()
    {
        rangeVB.Visible();
    }

    public void InActiveRange()
    {
        rangeVB.Invisible();
    }

    
}
