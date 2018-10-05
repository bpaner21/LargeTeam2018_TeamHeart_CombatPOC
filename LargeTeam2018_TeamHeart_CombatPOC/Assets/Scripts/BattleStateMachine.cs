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

    public HeroGUI Input;

    public List<GameObject> HerosToManage = new List<GameObject>();
    private HandleTurn heroChoice;

    public GameObject EnemyButton;
    public Transform Spacer;

	// Use this for initialization
	void Start ()
    {
        BattleStates = PerformAction.WAIT;
        HerosInBattle.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
        EnemysInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

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

                    }

                    BattleStates = PerformAction.PERFORMACTION;

                    break;
                }
            case (PerformAction.PERFORMACTION):
                {
                    break;
                }
        }
	}

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
}
