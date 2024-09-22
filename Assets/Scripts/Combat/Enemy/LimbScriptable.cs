using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Limb", menuName =  "Combat/Enemy/Limb", order = 1)]
public class LimbScriptable : ScriptableObject
{
   
    [Range(0.0f, 1f)] public float Dodge; 
        
    public float Initiative;
    [Header("0 for action that happens every turn")]
    public float ActionFrequency = 1;    
    
    public float  HP;

    [Range(0f, 1f)]
    public float BludgeoningResistance, SlashingResistance, StabbingResistance, DivineResistance, ElementResistance;

    public bool Critical;
    public bool Independant;

    //   public List<CombatAbility> Abilities;


}
