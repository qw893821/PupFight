using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibility : MonoBehaviour {

    private void Start()
    {
    }

    public void Visible()
    {
        transform.gameObject.SetActive(true);
    }

    public void Invisible()
    {
        transform.gameObject.SetActive(false);
    }
}
