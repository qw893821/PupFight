using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionManager : MonoBehaviour {
    private static EnemyActionManager _eAManager;
    public static EnemyActionManager eAmanager
    {
        get { return _eAManager; }
    }

    public GameObject[] enemies;
    public EnemyAction[] enemyActions;

    private void Awake()
    {
        if (_eAManager != null)
        {
            Destroy(this);
        }
        _eAManager = this;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyActions = new EnemyAction[enemies.Length];
    }

    // Use this for initialization
    void Start ()
    {
        for(int i = 0; i < enemies.Length; i++)
        {
            enemyActions[i] = enemies[i].GetComponent<EnemyAction>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
