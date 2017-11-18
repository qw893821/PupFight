using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActionManager : MonoBehaviour
{


    private static PlayerActionManager _instance;
    public static PlayerActionManager instance
    {
        get { return _instance; }
    }


    public GameObject playerActionUI;

    public GameObject targetGrid;//change from static to normal

    public GameObject selectedGO;
    //public above go for easy prototyoe

    PlayerAction playerAction;
    //several buttons
    Button moveButton;
    Button attactButton;
    Button statusButton;

    Button endTurn;

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

    private void Awake()
    {
        if (_instance != null) { Destroy(this); }
        _instance = this;
    }

    // Use this for initialization
    void Start()
    {
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
    void LateUpdate()
    {
        PupPick();
        ActiveUI();
       
        EnAbleRnage();
        RightMouseClick();
        CheckPrint();
    }

    public void ActiveUI()//active player action UI
    {
        if (!Selected())
        {
            timer = 0;//this timer avoid the UI being active when click
        }
        if (Selected())
        {
            timer += Time.deltaTime;
            if (Input.GetButtonDown("Fire1") && timer >= popTime)
            {

                if (!playerActionUI.activeSelf)
                {
                    GridSpec gSpec;
                    gSpec = targetGrid.GetComponent<GridSpec>();
                    if (gSpec.HighLighted() || gSpec.gStatus == "isEnemy")//if the grid is in moveable area
                    {
                        if (gSpec.gStatus == "isNeutral")//player could move to neutral grid and check player status
                        {
                            playerActionUI.SetActive(true);
                            moveButton.interactable = true;
                            attactButton.interactable = false;
                            playerActionUI.transform.position = Input.mousePosition;
                            Moved();
                        }
                        else if (gSpec.gStatus == "isEnemy")//player could attack grid have enemy and check player or enemy status
                        {
                            print("This is Enemy");
                            playerActionUI.SetActive(true);
                            moveButton.interactable = false;
                            attactButton.interactable = true;
                            playerActionUI.transform.position = Input.mousePosition;
                            enemyPup = targetGrid.transform.Find("Interactable").GetComponent<GridOccupy>().thisUnit;
                        }
                        else if (gSpec.gStatus == "isFriend")//player can only check player status when the grid is occupied by friend unit
                        {
                            playerActionUI.SetActive(true);
                            moveButton.interactable = false;
                            attactButton.interactable = false;
                            playerActionUI.transform.position = Input.mousePosition;
                        }
                    }
                    else if (!gSpec.HighLighted() && gSpec.gStatus != "isEnemy")
                    {
                        playerActionUI.SetActive(false);
                    }
                }
            }
        }

    }

    void Moved()//when selected character have done moving, this character could not move until next turn
    {
        if (selectedGO.GetComponent<PlayerAction>().aStatus == ActionStatus.moved)
        {
            moveButton.interactable = false;
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
        if (!Selected() && Input.GetButton("Fire1") && targetGrid != null)
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
                //wipe current selection
                DisableRange();
                selectedGO = null;
                targetGrid = null;
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

        }
    }

    public void MovePlayer()
    {
        PlayerAction playerAction;
        playerAction = selectedGO.GetComponent<PlayerAction>();
        playerAction.ChangeStatus();
    }

    void ShowStatus()
    {
        if (!Selected())
        {
            statusText.text = "Please Pick A Character";
        }
        else if (Selected())
        {
            statusText.text = "Charater Name: " + selectedGO.name;
        }
    }

    public void PickTargetGrid(GameObject go)
    {
        targetGrid = go;
    }

    public void EnemyAction()
    {
        //need let enemy do something here;
        //reset player character action status;
        FriendManager.friendManager.RestartTurn();
    }

    void EnAbleRnage()//local function enable range.
    {
        if (Selected())
        {
            selectedGO.GetComponent<PlayerAction>().ActiveRange();
        }
    }

    void DisableRange()
    {
        if (Selected())
        {
            selectedGO.GetComponent<PlayerAction>().InActiveRange();
        }
    }

    bool Selected()
    {
        if (selectedGO != null)
        {
            return true;
        }
        else return false;
    }


}
