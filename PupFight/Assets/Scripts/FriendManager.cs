using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendManager{
    static GameObject[] friends;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    static void GetPlayerTeam()
    {
        friends = GameObject.FindGameObjectsWithTag("Player");
        
    }

    static void RemovePlayer()
    {
        for(int i = 0; i < friends.Length; i++)
        {
            //if(friends[i])
        }
    }
}
