using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderThis : MonoBehaviour {
    public Renderer renderer;
	// Use this for initialization
	void Awake () {
        renderer = transform.GetComponent<Renderer>();
        renderer.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        DisableHighlight();

    }

    //enable highlight when enemy is in attack range
    private void OnTriggerEnter(Collider col)
    {
        if (transform.parent.parent.tag == "Player")
        {
            if (col.tag == "Enemy")
            {
                print(col.tag);
                renderer.enabled = true;

            }
        }
        if(transform.parent.parent.tag == "Enemy")
        {
            if(col.tag == "Player")
            {
                renderer.enabled = true;
            }
        }
        
    }

    //disable hightlight
    private void OnTriggerExit(Collider col)
    {
        if (transform.parent.parent.tag == "Player")
        {
            if (col.tag == "Enemy")
            {
                renderer.enabled = false;
            }
        }
        if(transform.parent.parent.tag == "Enemy")
        {
            if(col.tag == "Player")
            {
                renderer.enabled = false;
            }
        }
       
    }

    void DisableHighlight()//this function disable highlight when parent is not active
    {

        /*if (!transform.parent.gameObject.activeSelf)
        {
            print("disable");
            renderer.enabled = false;
        }*/
        if (renderer.enabled==true)//
        {
            if (transform.parent.parent.tag == "Player"&&PlayerActionManager.instance.selectedGO==null)
            {
                renderer.enabled = false;
            }
            
        }
    }
}
