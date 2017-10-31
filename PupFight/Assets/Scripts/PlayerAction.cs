using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum MovingState{
    isWaiting,
    isMoving
}
public class PlayerAction : MonoBehaviour {
    public GameObject targetGrid;
    GameObject playerActionUI;

    //player move speed;
    float speed;

    //several buttons


    //MovingStates
    MovingState mState;

    //camera ray, shot ray to get the grid where the mouse is.
    Ray camRay;

    // Use this for initialization
    void Start()
    {
        
        playerActionUI = GameObject.Find("PlayerActionUI");
        
        playerActionUI.SetActive(false);
        speed = 5f;
    }

    private void Update()
    {

    }
    // Update is called once per frame
    void LateUpdate () {
        ActiveUI();
        switch (mState) {
            case MovingState.isMoving:
                MovePlayer();
                break;
            case MovingState.isWaiting:
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
            mState = MovingState.isWaiting;  
        }
            
    }

    public void ChangeState()
    {
        mState = MovingState.isMoving;
    }
        
    

    public void ActiveUI()//active player action UI
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GridSpec gSpec;
            gSpec = targetGrid.GetComponent<GridSpec>();
            if (gSpec.gState == "isNetural")
            {
                playerActionUI.SetActive(true);
                playerActionUI.transform.position = Input.mousePosition;
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            playerActionUI.SetActive(false);

        }
    }

    
}
