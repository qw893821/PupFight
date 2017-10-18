using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {
    public GameObject targetGrid;
    GameObject playerActionUI;

    //several buttons
    

    // Use this for initialization
    void Start()
    {
        
        playerActionUI = GameObject.Find("PlayerActionUI");
        playerActionUI.SetActive(false);
    }

    private void Update()
    {
        
    }
    // Update is called once per frame
    void LateUpdate () {
        ActiveUI();
        print(targetGrid.transform.position);
    }

    

    public void MovePlayer()
    {
            transform.position = targetGrid.transform.position;
            transform.position += new Vector3(0, 0, 0);
    }

    public void ActiveUI()//active player action UI
    {
        if (Input.GetButtonDown("Fire1"))
        {
            
            playerActionUI.SetActive(true);
            playerActionUI.transform.position = Input.mousePosition;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            playerActionUI.SetActive(false);

        }
    }
}
