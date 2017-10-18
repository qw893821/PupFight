using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOccupy : MonoBehaviour {
    GameObject grid;
    GridSpec gridSpec;
	// Use this for initialization
	void Awake () {
        grid = transform.parent.gameObject;
        gridSpec = grid.GetComponent<GridSpec>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" || col.tag == "Enemy")
        {
            gridSpec.occupied = true;
            print("Enter");
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player" || col.tag == "Enemy")
        {
            gridSpec.occupied = false;
        }
    }
}
