using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActionManager : MonoBehaviour {
    public GameObject playerActionUI;

    PlayerAction playerAction;
    //several buttons
    Button moveButton;
    Button attactButton;
    Button statusButton;

    //pick pup
    GameObject playerPup;//player pup
    GridOccupy go;
    GameObject enemyPup;//enemy pup
    EnemyHealth enemyHealth;

    // Use this for initialization
    void Start () {
        moveButton = playerActionUI.transform.Find("MoveButton").gameObject.GetComponent<Button>();
        attactButton = playerActionUI.transform.Find("AttackButton").gameObject.GetComponent<Button>();
        statusButton = playerActionUI.transform.Find("StatusButton").gameObject.GetComponent<Button>();
        playerActionUI.SetActive(false);
    }
	
	// Update is called once per frame
	void LateUpdate () {
        ActiveUI();
        
    }

    public void ActiveUI()//active player action UI
    {

        if (Input.GetButtonDown("Fire1"))
        {
            if (!playerActionUI.activeSelf)
            {
                GridSpec gSpec;
                GetTargetPup();
                gSpec = playerAction.targetGrid.transform.parent.gameObject.GetComponent<GridSpec>();
                if (gSpec.gStatus == "isNeutral")//player could move to neutral grid and check player status
                {
                    playerActionUI.SetActive(true);
                    moveButton.interactable = true;
                    attactButton.interactable = false;
                    playerActionUI.transform.position = Input.mousePosition;
                }
                else if (gSpec.gStatus == "isEnemy")//player could attack grid have enemy and check player or enemy status
                {
                    playerActionUI.SetActive(true);
                    moveButton.interactable = false;
                    attactButton.interactable = true;
                    playerActionUI.transform.position = Input.mousePosition;
                    


                }
                else if (gSpec.gStatus == "isFriend")//player can only check player status when the grid is occupied by friend unit
                {
                    playerActionUI.SetActive(true);
                    moveButton.interactable = false;
                    attactButton.interactable = false;
                    playerActionUI.transform.position = Input.mousePosition;
                }
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            playerActionUI.SetActive(false);

        }
    }

    public void Damage()
    {
        
        playerAction = playerPup.GetComponent<PlayerAction>();
        enemyHealth = enemyPup.GetComponent<EnemyHealth>();

        enemyHealth.currentHealth -= playerAction.attackPow;
        playerActionUI.SetActive(false);
    }

    void GetTargetPup()
    {
        go = playerAction.targetGrid.GetComponent<GridOccupy>();
        //enemyPup = gO.thisUnit;
        if (go.thisUnit.tag == "Player")
        {
            playerPup = go.thisUnit;
        }
        if (go.thisUnit.tag == "Enemy")
        {
            enemyPup = go.thisUnit;
        }

    }


}
