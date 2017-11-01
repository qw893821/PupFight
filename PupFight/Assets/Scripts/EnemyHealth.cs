using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {
    //enemy health properities
    float maxHealth;
    public float currentHealth;

    //health ui
    public Image healthImg;
    RectTransform imgRT;

    //player action UI.
    GameObject playerActionUI;

    //player attackbility
    public GameObject player;//need change when in late stage when there are mulity characters;
    PlayerAction playerAction;
    // Use this for initialization
    void Start () {
        maxHealth = 200f;
        currentHealth = maxHealth;
        imgRT = healthImg.GetComponent<RectTransform>();
        playerAction = player.GetComponent<PlayerAction>();
        playerActionUI = GameObject.Find("PlayerActionUI");


    }
	
	// Update is called once per frame
	void Update () {
        HealthBarChange();
    }

    void HealthBarChange()
    {
        float healthRate;
        healthRate = currentHealth / maxHealth;
        imgRT.localScale = new Vector3(healthRate, 1, 1);

    }

    
}
