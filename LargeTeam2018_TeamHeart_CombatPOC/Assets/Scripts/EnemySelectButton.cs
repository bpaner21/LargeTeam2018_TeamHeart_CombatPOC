using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelectButton : MonoBehaviour
{
    public GameObject EnemyPrefab;

    public void SelectEnemy()
    {
        GameObject.Find("Battle Manager").GetComponent<BattleStateMachine>();   // save input of enemy prefab
    }
}
