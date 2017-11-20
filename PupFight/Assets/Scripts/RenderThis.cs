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
        if (col.tag == "Enemy")
        {
            renderer.enabled = true;
            
        }
    }

    //disable hightlight
    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Enemy")
        {
            renderer.enabled = false;
        }
    }

    void DisableHighlight()//this function disable highlight when parent is not active
    {

        /*if (!transform.parent.gameObject.activeSelf)
        {
            print("disable");
            renderer.enabled = false;
        }*/
        if (renderer.enabled==true&&PlayerActionManager.instance.selectedGO == null)//
        {
            renderer.enabled = false;
        }
    }
}
