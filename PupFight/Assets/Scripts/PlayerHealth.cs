using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    //player health properities
    float maxHealth;
    public float currentHealth;

    //health ui
    public Image healthImg;
    RectTransform imgRT;



    // Use this for initialization
    void Start() {
        maxHealth = 200f;
        currentHealth = maxHealth;
        imgRT = healthImg.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update() {
        HealthBarChange();
    }


    void HealthBarChange()
    {
        float healthRate;
        healthRate = currentHealth / maxHealth;
        imgRT.localScale = new Vector3(healthRate, 1, 1);
        
    }


}
