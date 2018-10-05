using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HandleTurn
{
    public string Type;
    public string Attacker; // name of attacker
    public GameObject AttackersGameObject;  // who attacks
    public GameObject AttackersTarget;      // who gets attacked

    // which attack is performed
}
