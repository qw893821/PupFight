using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActionManager : MonoBehaviour {
    public GameObject playerActionUI;

    public static GameObject targetGrid;

    public static GameObject selectedGO;
    PlayerAction playerAction;
    //several buttons
    Button moveButton;
    Button attactButton;
    Button statusButton;

    //player status ui
    public Text statusText;

    //UI show standby timer
    float timer;
    float popTime;

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
        timer = 0;//this timer use to avoid the unexpected ui pop when select player;
        popTime = 0.5f;
    }

    private void Update()
    {
        //CheckPrint();
        ShowStatus();
    }

    // Update is called once per frame
    void LateUpdate () {
        
        
        PupPick();
        ActiveUI();
        RightMouseClick();
    }

    public void ActiveUI()//active player action UI
    {
        if (selectedGO == null)
        {
            timer = 0;
        }
        if (selectedGO != null)
        {
            
            timer += Time.deltaTime;
            if (Input.GetButtonDown("Fire1")&&timer>=popTime)
            {
                
                if (!playerActionUI.activeSelf)
                {
                    GridSpec gSpec;
                    gSpec = targetGrid.GetComponent<GridSpec>();
                    print(gSpec.gStatus);
                    if (gSpec.gStatus == "isNeutral")//player could move to neutral grid and check player status
                    {
                        print("THIS GRID IS NEUTRAL");
                        playerActionUI.SetActive(true);
                        moveButton.interactable = true;
                        attactButton.interactable = false;
                        playerActionUI.transform.position = Input.mousePosition;
                    }
                    else if (gSpec.gStatus == "isEnemy")//player could attack grid have enemy and check player or enemy status
                    {
                        print("THIS GRID IS BAD");
                        playerActionUI.SetActive(true);
                        moveButton.interactable = false;
                        attactButton.interactable = true;
                        playerActionUI.transform.position = Input.mousePosition;
                        enemyPup = targetGrid.transform.Find("Interactable").GetComponent<GridOccupy>().thisUnit;
                    }
                    else if (gSpec.gStatus == "isFriend")//player can only check player status when the grid is occupied by friend unit
                    {
                        print("THIS GRID IS GOOD");
                        playerActionUI.SetActive(true);
                        moveButton.interactable = false;
                        attactButton.interactable = false;
                        playerActionUI.transform.position = Input.mousePosition;
                    }
                }
            }
        }
        
    }

    public void AttackEnemy()
    {
        float playerAttackPow;
        playerAttackPow = selectedGO.GetComponent<PlayerAction>().attackPow;

        enemyHealth = enemyPup.GetComponent<EnemyHealth>();

        enemyHealth.GetDamage(playerAttackPow);
        selectedGO.GetComponent<PlayerAction>().Attacked();
        playerActionUI.SetActive(false);
        selectedGO = null;

    }

    void PupPick()
    {
        if (selectedGO == null && Input.GetButton("Fire1"))
        {
            
            
                GridSpec gSpec;
                gSpec = targetGrid.GetComponent<GridSpec>();
                if (gSpec.gStatus == "isFriend")
                {
                    selectedGO = targetGrid.transform.Find("Interactable").GetComponent<GridOccupy>().thisUnit;
                    if (selectedGO.GetComponent<PlayerAction>().FinishAction())
                    {
                        selectedGO = null;
                    }
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

    //set this function to check if get the target grid. for bug fix use only
    void CheckPrint()
    {
        if (Input.GetButton("Fire1"))
        {
            print(selectedGO);
        }
    }

    public void MovePlayer()
    {
        PlayerAction playerAction;
        playerAction = selectedGO.GetComponent<PlayerAction>();
        playerAction.ChangeStatus();
        print(playerAction);
    }
    
    void ShowStatus()
    {
        if (selectedGO == null)
        {
            statusText.text = "Please Pick A Character";
        }
        else if (selectedGO!=null)
        {
            statusText.text = "Charater Name: " + selectedGO;
        }
    }


}
