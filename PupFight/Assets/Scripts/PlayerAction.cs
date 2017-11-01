﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum ActionStatus{
    isWaiting,
    isMoving,
    isActing
}
public class PlayerAction : MonoBehaviour {
    public GameObject targetGrid;
    GameObject playerActionUI;
    Button moveButton;
    Button attactButton;
    Button statusButton;

    //player move speed;
    float speed;

    //player attack
    GridOccupy gO;
    GameObject enemyPup;
    float attackPow;
    EnemyHealth enemyHealth;

    //several buttons


    //MovingStatus
    ActionStatus aStatus;

    //camera ray, shot ray to get the grid where the mouse is.
    Ray camRay;
    RaycastHit hit;

    // Use this for initialization
    void Start()
    {

        playerActionUI = GameObject.Find("PlayerActionUI");
        playerActionUI.SetActive(false);
        moveButton = playerActionUI.transform.Find("MoveButton").gameObject.GetComponent<Button>();
        attactButton = playerActionUI.transform.Find("AttackButton").gameObject.GetComponent<Button>();
        statusButton = playerActionUI.transform.Find("StatusButton").gameObject.GetComponent<Button>();
        speed = 5f;
        attackPow = 5f;
        

    }

    private void Update()
    {

    }
    // Update is called once per frame
    void LateUpdate() {
        ActiveUI();
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
            aStatus = ActionStatus.isWaiting;
        }

    }

    public void ChangeState()
    {
        aStatus = ActionStatus.isMoving;
    }



    public void ActiveUI()//active player action UI
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            if (!playerActionUI.activeSelf)
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
                    GetTargetPup();


                }
                else if (gSpec.gStatus == "isFriend")//player can only check player status when the grid is occupied by friend unit
                {
                    playerActionUI.SetActive(true);
                    moveButton.interactable = false;
                    attactButton.interactable = false;
                    playerActionUI.transform.position = Input.mousePosition;
                }
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            playerActionUI.SetActive(false);

        }
    }

    void MovingCheck()
    {
        switch (aStatus)
        {
            case ActionStatus.isMoving:
                MovePlayer();
                break;
            case ActionStatus.isWaiting:
                break;

        }
    }

    void AttackCheck()
    {

    }



    void GetTargetPup()
    {
        gO = targetGrid.GetComponent<GridOccupy>();
        enemyPup = gO.thisUnit;
        //print(enemyPup);
        
    }

    public void Damage()
    {
        enemyHealth = enemyPup.GetComponent<EnemyHealth>();
        enemyHealth.currentHealth -= attackPow;
        playerActionUI.SetActive(false);
    }
}
