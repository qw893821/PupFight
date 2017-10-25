using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridState
{
    isEnemy,
    isFriend,
    isNetural
}

public class GridSpec: MonoBehaviour {
    Ray camRay;
    RaycastHit hit;

    //get player;
    PlayerAction playerAction;
    GameObject player;

    //if the grid had an obejct setted, this grid will not highlighted
    public bool occupied;

    //gird state effect player possible action
    GridState gState;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAction = player.GetComponent<PlayerAction>();
        occupied = false;

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
                    if (Input.GetButtonDown("Fire1"))
                    {
                        playerAction.targetGrid = hit.collider.gameObject;
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
    private void OnTriggerEnter(Collider col)
    {
        switch (col.tag)
        {
            case "Player":
                gState = GridState.isFriend;
                break;
            case "Enemy":
                gState = GridState.isEnemy;
                break;
            default:
                gState = GridState.isNetural;
                break;
        }
    }
}
