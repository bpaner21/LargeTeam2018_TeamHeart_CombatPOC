using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroStateMachine : MonoBehaviour
{
    private BattleStateMachine bsm;
    public BaseHero Hero;

    public enum TurnState
    {
        PROCESSING,
        ADDTOLIST,
        WAITING,
        SELECTING,
        ACTION,
        DEAD
    }

    public TurnState CurrentState;

    private float cur_cooldown = 0f;
    private float max_cooldown = 5f;

    public Image ProgressBar;

	// Use this for initialization
	void Start ()
    {
        bsm = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        CurrentState = TurnState.PROCESSING;

    }   // VOID START

    // Update is called once per frame
    void Update ()
    {
        Debug.Log(CurrentState);
        switch (CurrentState)
        {
            case (TurnState.PROCESSING):
                {
                    UpgradeProgressBar();
                    break;
                }
            case (TurnState.ADDTOLIST):
                {
                    bsm.HerosToManage.Add(this.gameObject);
                    CurrentState = TurnState.WAITING;
                    break;
                }
            case (TurnState.WAITING):
                {
                    // idle state
                    break;
                }
            /*case (TurnState.SELECTING):
                {
                    break;
                }//*/
            case (TurnState.ACTION):
                {
                    break;
                }
            case (TurnState.DEAD):
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
	}   // VOID UPDATE

    void UpgradeProgressBar()
    {
        cur_cooldown = cur_cooldown + Time.deltaTime;
        float calc_cooldown = cur_cooldown / max_cooldown;
        ProgressBar.transform.localScale = new Vector3(Mathf.Clamp(calc_cooldown, 0, 1), ProgressBar.transform.localScale.y, ProgressBar.transform.localScale.z);
        if(cur_cooldown >= max_cooldown)
        {
            CurrentState = TurnState.ADDTOLIST;
        }

    }   // VOID UPGRADEPROGRESSBAR
}   // PUBLIC CLASS
