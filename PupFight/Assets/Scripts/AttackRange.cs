using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour {
    public GameObject rangeHL;
    Vector3[] locations;
    Renderer rangeHLRenderer;
    // Use this for initialization
    void Start() {
        rangeHLRenderer = rangeHL.GetComponent<Renderer>();
        rangeHLRenderer.enabled = false;
        InstantiateRange();

    }

    // Update is called once per frame
    void Update() {

    }

    void InstantiateRange()
    {

        SetLocation(2);//"2" is a temp data for protortype. which means the attack range is 2 grid.
        for (int i = 0; i < locations.Length; i++)
        {
            Instantiate(rangeHL, locations[i], Quaternion.identity, transform);
        }
    }

    void SetLocation(int i)//this function storge attack location data. 
    {
        locations = new Vector3[4 * i];
        //four direction location add. may find math to solve, now manual add
        for (int x = 0; x < i; x++)
        {
            //!!!1f is based on only useful in current protortype, becuase the grid is (1,1,1) sized.
            locations[4 * x] = transform.position + new Vector3(0 + (x + 1) * 1f, 0.02f, 0);
            locations[4 * x + 1] = transform.position + new Vector3(0 - (x + 1) * 1f, 0.02f, 0);
            locations[4 * x + 2] = transform.position + new Vector3(0, 0.02f, 0 + (x + 1) * 1f);
            locations[4 * x + 3] = transform.position + new Vector3(0, 0.02f, 0 - (x + 1) * 1f);
        }
    }

}
