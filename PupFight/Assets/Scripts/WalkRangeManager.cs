using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkRangeManager :MonoBehaviour{
    private static WalkRangeManager _walkRangeInstance;
    public static WalkRangeManager walkRangeInstance
    {
        get { return _walkRangeInstance; }
    }
    public GameObject[] moveArea1;//move ability is "1"
    public GameObject[] moveArea2;//move ability is "2"
    private void Awake()
    {
        if (_walkRangeInstance != null)
        {
            Destroy(this);
        }
        _walkRangeInstance = this;
    }
    private void Start()
    {
        print(moveArea1);
    }

}
