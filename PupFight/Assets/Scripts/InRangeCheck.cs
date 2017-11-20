using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRangeCheck : MonoBehaviour {
    public GameObject[] grids;
    Renderer[] renderers;

    public bool haveInRange;

    //get referrence before the GO is set inacitve.
    private void Awake()
    {
        renderers = new Renderer[grids.Length];
        for (int i = 0; i < grids.Length; i++)
        {
            renderers[i] = grids[i].GetComponent<Renderer>();
        }
    }

    private void Update()
    {
        InCheck();
        
    }

    void InCheck()
    {
        foreach(Renderer rend in renderers)
        {
            if (rend.enabled == true)
            {
                haveInRange = true;
                print("got one");
                break;
            }
        }
        
    }
}
