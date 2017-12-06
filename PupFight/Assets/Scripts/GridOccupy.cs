using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOccupy : MonoBehaviour {
    GameObject grid;
    GridSpec gridSpec;

    //this unit is the one whihc occpuy this grid;
    public GameObject thisUnit;

    EnemyAction eA;
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
        if (gridSpec.occupied != true)
        {
            switch (col.tag)
            {
                case "Player":
                    gridSpec.gStatus = "isFriend";
                    break;
                case "Enemy":
                    gridSpec.gStatus = "isEnemy";
                    
                    break;

            }
            if (col.tag == "Player" || col.tag == "Enemy")
            {
                gridSpec.occupied = true;
                thisUnit = col.transform.gameObject;
                if (col.tag == "Enemy")
                {
                    eA = thisUnit.GetComponent<EnemyAction>();
                    eA.currentGrid = transform.parent.gameObject;
                }
            }
            //thisUnit = col.transform.gameObject;
            else { thisUnit = null; }
        }
        
    }


    private void OnTriggerExit(Collider col)
    {
        if (col.transform.gameObject == thisUnit)
        {
            if (col.tag == "Player" || col.tag == "Enemy")
            {
                gridSpec.occupied = false;
                gridSpec.gStatus = "isNeutral";
                thisUnit = null;
            }
        }
        
    }

    
    public GameObject ReturnGO()//give this gameobject to the "player"/"enemy"
    {
        return this.transform.gameObject;
    }
}
