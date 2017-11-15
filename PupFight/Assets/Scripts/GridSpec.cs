using System.Collections;
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
        
        HLMoveArea(2, PlayerActionManager.instance.selectedGO);
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
                //current do not need to highlight grid when player are not selected, so comment this code now. may use in later version
                //hit.collider.gameObject.GetComponent<Renderer>().enabled = true;
                if (Input.GetButton("Fire1"))
                {
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


    //this function highlight the grid where player could enter
    //need more code to enable the ui when in area, not in this script
    void HLMoveArea(int i,GameObject go)
    {
        if (go != null)
        {
            Vector3 offset;
            offset = transform.position - go.transform.position;

            if ((Mathf.Abs(offset.x) / 1 + Mathf.Abs(offset.z) / 1) <= i)
            {
                 transform.Find("Interactable").gameObject.GetComponent<Renderer>().enabled = true;
            }
            
            
        }
    }

    public bool HighLighted()
    {
        if(transform.Find("Interactable").gameObject.GetComponent<Renderer>().enabled == true)
        {
            return true;
        }
        else { return false; }
    }
}
