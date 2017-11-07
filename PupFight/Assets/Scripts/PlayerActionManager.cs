using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActionManager : MonoBehaviour {
    public GameObject playerActionUI;

    public GameObject targetGrid;

    public GameObject selectedGO;
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

    bool playerPicked;//check if have one player have been selected

    // Use this for initialization
    void Start () {
        moveButton = playerActionUI.transform.Find("MoveButton").gameObject.GetComponent<Button>();
        attactButton = playerActionUI.transform.Find("AttackButton").gameObject.GetComponent<Button>();
        statusButton = playerActionUI.transform.Find("StatusButton").gameObject.GetComponent<Button>();
        playerActionUI.SetActive(false);
        playerPicked = false;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        ActiveUI();
        RightMouseClick();
        PupPick();
    }

    public void ActiveUI()//active player action UI
    {
        if (selectedGO != null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (!playerActionUI.activeSelf)
                {
                    GridSpec gSpec;
                    gSpec = targetGrid.GetComponent<GridSpec>();
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
        }
        
    }

    public void Damage()
    {
        
        playerAction = playerPup.GetComponent<PlayerAction>();
        enemyHealth = enemyPup.GetComponent<EnemyHealth>();

        enemyHealth.currentHealth -= playerAction.attackPow;
        playerActionUI.SetActive(false);
        selectedGO = null;
    }

    void PupPick()
    {
        if (Input.GetButton("Fire1"))
        { 
            GridSpec gSpec;
            gSpec = targetGrid.GetComponent<GridSpec>();
        
        
            if (selectedGO == null && gSpec.gStatus == "isFriend")
            {
                selectedGO = targetGrid.transform.Find("Interactable").GetComponent<GridOccupy>().thisUnit;
            }

        }
        
    }


    void RightMouseClick()
    {
        if (!playerActionUI.activeSelf)
        {
            if (Input.GetButton("Fire2"))
            {
                selectedGO = null;
            }
        }
        if (playerActionUI.activeSelf)
        {
            if (Input.GetButton("Fire2"))
            {
                playerActionUI.SetActive(false);

            }
        }
    }
}
