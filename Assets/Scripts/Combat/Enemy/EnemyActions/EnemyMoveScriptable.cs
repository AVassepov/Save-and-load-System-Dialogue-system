using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAction", menuName = "Combat/Enemy/Action", order = 1)]
public class EnemyMoveScriptable : ScriptableObject
{

    public ActionType Type;
    
    
    
    public enum ActionType{
        Attack,
        Block,
        Debuffing,
        Buffing,
        Special,
        Talk
    }
    public Character.DamageType DamageType;
    
    [Range(0.0f, 1f)]
    public float Accuracy;

    public float Damage;
    public float DamageVariance;

    public bool SaveRoll;
    
    public bool AOE;
    
    
    [Range(0.0f, 1f)]
    public float CritRate;

    public List<Debuff> Effects;

}



[System.Serializable]
public struct Debuff
{
    public Character.Affliction Afflicton;
    
    [Range(0.0f, 1f)]
    public float ApplicationChance;

}

[System.Serializable]
public struct SaveRollData
{
    public int RollDC;
    public bool NonDeathBadEnd;
    
}