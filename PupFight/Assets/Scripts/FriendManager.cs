using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendManager:MonoBehaviour{
    private static FriendManager _friendmanager;
    public static FriendManager friendManager
    {
        get { return _friendmanager; }
    }

    public GameObject[] friends;
    public PlayerAction[] playerActions;
	// Use this for initialization
    //start() is after awake()
	void Start ()
    {
        GetPlayerAction();
    }

    private void Awake()
    {
        if (_friendmanager != null)
        { Destroy(this); }
        _friendmanager = this;
        GetPlayerTeam();

        playerActions = new PlayerAction[friends.Length];
    }

    // Update is called once per frame
    void Update () {

    }

    void GetPlayerTeam()
    {
        friends = GameObject.FindGameObjectsWithTag("Player");
    }

    void GetPlayerAction()
    {
        for (int i = 0; i < friends.Length; i++)
        {
            playerActions[i] = friends[i].GetComponent<PlayerAction>();
            
        }
        
    }

    bool EndCheck()
    {
        for(int i = 0; i < friends.Length; i++)
        {
            if (playerActions[i].aStatus != ActionStatus.isDone)
            {
                return false;
            }   
        }

        return true;
    }

    public void RestartTurn()
    {
        for(int i = 0; i < friends.Length; i ++)
        {
            playerActions[i].aStatus = ActionStatus.isWaiting;
        }
    }


}
