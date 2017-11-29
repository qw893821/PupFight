using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionManager : MonoBehaviour {
    private static EnemyActionManager _eAManager;
    public static EnemyActionManager eAmanager
    {
        get { return _eAManager; }
    }

    public GameObject[] moveArea1;//move ability is "1"
    public GameObject[] moveArea2;//move ability is "2"

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
        int i = 0;//dynamic name of the range2area clone
        foreach(GameObject go in moveArea1)
        {
            GameObject clone;
            clone=Instantiate(go, transform);
            clone.name = "Range2Area" + i;
            i++;
        }
        int x = 0;//dynamic name of the rang3area clone
        foreach (GameObject go in moveArea2)
        {
            GameObject clone;
            clone = Instantiate(go, transform);
            clone.name = "Range3Area" + x;
            x++;
        }
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

    public void EnemyTurn()//when enemy turn starts, all enemy "ActionStatus" set "isWaiting"
    {
        foreach(EnemyAction eA in enemyActions)
        {
            eA.SetWaiting();
        }
    }
}
