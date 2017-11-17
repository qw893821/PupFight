using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour {
    public GameObject rangeHL;
    Vector3[] locations;

	// Use this for initialization
	void Start () {
        InstantiateRange();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void InstantiateRange()
    {
        
        SetLocation(2);
        print(locations[7]);
        for(int i = 0; i < locations.Length; i++)
        {
            Instantiate(rangeHL, locations[i],Quaternion.identity,transform);
        }
    }

    void SetLocation(int i)
    {
        locations = new Vector3[4 * i];
        //four direction location add. may find math to solve, now manual add
        for(int x=0; x< i; x++)
        {
            locations[4 * x] = transform.position + new Vector3(0+(x + 1)*1f, 0.02f, 0);
            locations[4 * x + 1] = transform.position + new Vector3(0-(x+1)*1f, 0.02f, 0);
            locations[4 * x + 2] = transform.position + new Vector3(0, 0.02f, 0+(x+1)*1f);
            locations[4 * x + 3] = transform.position + new Vector3(0, 0.02f,0-(x+1)*1f);
        }
    }
}
