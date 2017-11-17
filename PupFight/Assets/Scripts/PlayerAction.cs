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
public class PlayerAction : MonoBehaviour {

    //player move speed;
    float speed;
    
    //pup attack power
    public float attackPow;

    //MovingStatus
    public ActionStatus aStatus;

    //range GO
    public GameObject[] rangeGOs;


    //camera ray, shot ray to get the grid where the mouse is.
    Ray camRay;
    RaycastHit hit;

    // Use this for initialization
    void Start()
    {


        this.gameObject.name = this.gameObject.name.ToString();
        speed = 5f;
        attackPow = 5f;

        
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

    public void EnableRange()
    {

    }

    

    
}
