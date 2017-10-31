using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum MovingStatus{
    isWaiting,
    isMoving
}
public class PlayerAction : MonoBehaviour {
    public GameObject targetGrid;
    GameObject playerActionUI;
    Button moveButton;
    Button attactButton;
    Button statusButton;

    //player move speed;
    float speed;

    //several buttons


    //MovingStatus
    MovingStatus mStatus;

    //camera ray, shot ray to get the grid where the mouse is.
    Ray camRay;

    // Use this for initialization
    void Start()
    {
        
        playerActionUI = GameObject.Find("PlayerActionUI");
        playerActionUI.SetActive(false);
        moveButton= playerActionUI.transform.Find("MoveButton").gameObject.GetComponent<Button>();
        attactButton = playerActionUI.transform.Find("AttackButton").gameObject.GetComponent<Button>();
        statusButton = playerActionUI.transform.Find("StatusButton").gameObject.GetComponent<Button>();
        speed = 5f;
        
    }

    private void Update()
    {

    }
    // Update is called once per frame
    void LateUpdate () {
        ActiveUI();
        switch (mStatus) {
            case MovingStatus.isMoving:
                MovePlayer();
                break;
            case MovingStatus.isWaiting:
                break;

        }
        
    }

    

    public void MovePlayer()
    {
        Vector3 target;
        float offset;
        
        offset = transform.position.y - targetGrid.transform.position.y;
        target = new Vector3(targetGrid.transform.position.x, targetGrid.transform.position.y + offset, targetGrid.transform.position.z);

        transform.position=Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position == target)
        {
            mStatus = MovingStatus.isWaiting;  
        }
            
    }

    public void ChangeState()
    {
        mStatus = MovingStatus.isMoving;
    }
        
    

    public void ActiveUI()//active player action UI
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GridSpec gSpec;
            gSpec = targetGrid.transform.parent.gameObject.GetComponent<GridSpec>();
            if (gSpec.gStatus == "isNeutral")//player could move to neutral grid and check player status
            {
                playerActionUI.SetActive(true);
                moveButton.interactable = true;
                attactButton.interactable = false;
                playerActionUI.transform.position = Input.mousePosition;
            }
            else if (gSpec.gStatus == "isEnemy")//player could attack grid have enemy and check player or enemy status
            {
                playerActionUI.SetActive(true);
                moveButton.interactable = false;
                attactButton.interactable = true;
                playerActionUI.transform.position = Input.mousePosition;
            }
            else if (gSpec.gStatus == "isFriend")//player can only check player status when the grid is occupied by friend unit
            {
                playerActionUI.SetActive(true);
                moveButton.interactable = false;
                attactButton.interactable = false;
                playerActionUI.transform.position = Input.mousePosition;
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            playerActionUI.SetActive(false);

        }
    }

    
}
