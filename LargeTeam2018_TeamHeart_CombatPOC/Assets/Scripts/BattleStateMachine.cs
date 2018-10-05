using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStateMachine : MonoBehaviour
{
    public enum PerformAction
    {
        WAIT,
        TAKEACTION,
        PERFORMACTION
    }

    public PerformAction BattleStates;

    public List<HandleTurn> PerformList = new List<HandleTurn>();
    public List<GameObject> HerosInBattle = new List<GameObject>();
    public List<GameObject> EnemysInBattle = new List<GameObject>();

    public enum HeroGUI
    {
        ACTIVATE,
        WAITING,
        INPUT1,
        INPUT2,
        DONE
    }

    public HeroGUI HeroInput;

    public List<GameObject> HerosToManage = new List<GameObject>();
    private HandleTurn heroChoice;

    public GameObject EnemyButton;
    public Transform Spacer;

    public GameObject AttackPanel;
    public GameObject EnemySelectPanel;

	// Use this for initialization
	void Start ()
    {
        BattleStates = PerformAction.WAIT;
        HerosInBattle.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
        EnemysInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        HeroInput = HeroGUI.ACTIVATE;

        AttackPanel.SetActive(false);
        EnemySelectPanel.SetActive(false);

        EnemyButtons();
	}
	
	// Update is called once per frame
	void Update ()
    {
		switch (BattleStates)
        {
            case (PerformAction.WAIT):
                {
                    if(PerformList.Count > 0)
                    {
                        BattleStates = PerformAction.TAKEACTION;
                    }
                    break;
                }
            case (PerformAction.TAKEACTION):
                {
                    GameObject performer = GameObject.Find(PerformList[0].Attacker);

                    if (PerformList[0].Type == "Enemy")
                    {
                        EnemyStateMachine esm = performer.GetComponent<EnemyStateMachine>();
                        esm.HeroToAttack = PerformList[0].AttackersTarget;
                        esm.CurrentState = EnemyStateMachine.TurnState.ACTION;
                    }
                    if (PerformList[0].Type == "Hero")
                    {
                        HeroStateMachine hsm = performer.GetComponent<HeroStateMachine>();
                        hsm.EnemyToAttack = PerformList[0].AttackersTarget;
                        hsm.CurrentState = HeroStateMachine.TurnState.ACTION;
                    }

                    BattleStates = PerformAction.PERFORMACTION;

                    break;
                }
            case (PerformAction.PERFORMACTION):
                {
                    break;
                }
        }   // switch (BattleStates)

        switch (HeroInput)
        {
            case (HeroGUI.ACTIVATE):
                {
                    if (HerosToManage.Count > 0)
                    {
                        HerosToManage[0].transform.Find("Selector").gameObject.SetActive(true);
                        heroChoice = new HandleTurn();
                        AttackPanel.SetActive(true);
                        HeroInput = HeroGUI.WAITING;
                    }
                    break;
                }
            case (HeroGUI.WAITING):
                {
                    // idle state

                    break;
                }
            case (HeroGUI.INPUT1):
                {
                    break;
                }
            case (HeroGUI.INPUT2):
                {
                    break;
                }
            case (HeroGUI.DONE):
                {
                    HeroInputDone();
                    break;
                }
        }   // switch (HeroInput)

    }   // void Update()

    public void CollectActions(HandleTurn input)
    {
        PerformList.Add(input);
    }

    private void EnemyButtons()
    {
        foreach(GameObject enemy in EnemysInBattle)
        {
            GameObject newButton = Instantiate(EnemyButton) as GameObject;
            EnemySelectButton button = newButton.GetComponent<EnemySelectButton>();

            EnemyStateMachine cur_enemy = enemy.GetComponent<EnemyStateMachine>();

            Text buttonText = newButton.transform.Find("Text").gameObject.GetComponent<Text>();
            buttonText.text = cur_enemy.Enemy.Name;

            button.EnemyPrefab = enemy;

            newButton.transform.SetParent(Spacer, false);
        }
    }

    public void Input1()    // Attack button
    {
        heroChoice.Attacker = HerosToManage[0].name;
        heroChoice.AttackersGameObject = HerosToManage[0];
        heroChoice.Type = "Hero";

        AttackPanel.SetActive(false);
        EnemySelectPanel.SetActive(true);
        Debug.Log("Input1");
    }

    public void Input2(GameObject ChosenEnemy)    // Enemy Selection
    {
        heroChoice.AttackersTarget = ChosenEnemy;
        HeroInput = HeroGUI.DONE;
        Debug.Log("Input2");
    }

    private void HeroInputDone()
    {
        PerformList.Add(heroChoice);
        EnemySelectPanel.SetActive(false);
        HerosToManage[0].transform.Find("Selector").gameObject.SetActive(false);
        HerosToManage.RemoveAt(0);
        HeroInput = HeroGUI.ACTIVATE;
        Debug.Log("HeroInputDone");
    }
}
