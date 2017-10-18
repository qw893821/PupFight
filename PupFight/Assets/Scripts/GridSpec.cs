using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpec: MonoBehaviour {
    Ray camRay;
    RaycastHit hit;

	// Use this for initialization
	void Start () {
		
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
            }

            
        }
        
        
    }
}
