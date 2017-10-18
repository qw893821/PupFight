using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public GameObject targetGrid;

	// Use this for initialization
	void Start()
    {
        targetGrid = null;
    }
	
	
	// Update is called once per frame
	void LateUpdate () {

    }

    

    public void MovePlayer()
    {
            transform.position = targetGrid.transform.position;
            transform.position += new Vector3(0, 0, 0);
    }
    
}
