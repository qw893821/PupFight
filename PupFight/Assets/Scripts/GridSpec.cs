using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpec: MonoBehaviour {
    Ray camRay;
    RaycastHit hit;

    //get player;
    PlayerMovement playerMove;
    GameObject player;

    //if the grid had an obejct setted, this grid will not highlighted
    public bool occupied;


	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMove = player.GetComponent<PlayerMovement>();
        occupied = false;

	}
	
	// Update is called once per frame
	void Update () {
        transform.Find("Interactable").gameObject.GetComponent<Renderer>().enabled = false;
        camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(camRay, out hit))
        {
                if (hit.collider.tag == "HighLight")
                {
                    hit.collider.gameObject.GetComponent<Renderer>().enabled = true;
                    playerMove.targetGrid = hit.collider.gameObject;

                }
            
        }
        
        
        
    }
    
}
