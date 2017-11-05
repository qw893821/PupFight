using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum ActionStatus{
    isWaiting,
    isMoving,
    moved,
    isActing
}
public class PlayerAction : MonoBehaviour {
    public GameObject targetGrid;        
    //player move speed;
    float speed;
    
    //pup attack power
    public float attackPow;

    //MovingStatus
    ActionStatus aStatus;

    //camera ray, shot ray to get the grid where the mouse is.
    Ray camRay;
    RaycastHit hit;

    // Use this for initialization
    void Start()
    {

        
        
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



    public void MovePlayer()
    {
        Vector3 target;
        float offset;

        offset = transform.position.y - targetGrid.transform.position.y;
        target = new Vector3(targetGrid.transform.position.x, targetGrid.transform.position.y + offset, targetGrid.transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position == target)
        {
            aStatus = ActionStatus.moved;
        }

    }

    public void ChangeState()
    {
        aStatus = ActionStatus.isMoving;
    }



    

    void MovingCheck()
    {
        switch (aStatus)
        {
            case ActionStatus.isMoving:
                MovePlayer();
                break;

        }
    }

    void AttackCheck()
    {

    }



    

    
}
