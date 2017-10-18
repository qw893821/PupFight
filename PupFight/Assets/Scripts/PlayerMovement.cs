using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    //camera shoot ray
    Ray camRay;
    GameObject targetGrid;
    RaycastHit hit;

	// Use this for initialization
	void Start () {
        targetGrid = null;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        MovePlayer();
        GetTargetGrid();

    }

    void GetTargetGrid()
    {
        camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(camRay, out hit))
        {
            if (hit.collider.tag == "Hihglight")
            {
                targetGrid = hit.collider.gameObject;
            }
        }
    }

    void MovePlayer()
    {
        transform.position = targetGrid.transform.position;
        transform.position += new Vector3(0, 0, 0);
    }
}
