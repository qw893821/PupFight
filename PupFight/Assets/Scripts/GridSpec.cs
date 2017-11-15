﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GridSpec: MonoBehaviour {
    Ray camRay;
    RaycastHit hit;

    //get player;

    //if the grid had an obejct setted, this grid will not highlighted
    public bool occupied;

    //gird state effect player possible action
    public string gStatus;
	// Use this for initialization
	void Start () {
        occupied = false;
        gStatus = "isNeutral";
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        HighLightGrid();
        
    }

    private void LateUpdate()
    {
        DisableHighLight();
    }

    //high light grid when it is not occupied
    void HighLightGrid()
    {
        transform.Find("Interactable").gameObject.GetComponent<Renderer>().enabled = false;
        if (Physics.Raycast(camRay, out hit))
            {

            if (hit.collider.tag == "HighLight")
            {
                    hit.collider.gameObject.GetComponent<Renderer>().enabled = true;
                if (Input.GetButton("Fire1"))
                {
                    print(PlayerActionManager.instance.targetGrid);
                    PlayerActionManager.instance.PickTargetGrid(hit.collider.gameObject.transform.parent.gameObject);
                }
            }
        } 
    }

    void DisableHighLight()
    {
        if (occupied)
        {
            transform.Find("Interactable").GetComponent<Renderer>().enabled = false;
        }
    }
    

}
