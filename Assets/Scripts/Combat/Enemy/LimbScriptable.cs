using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Limb", menuName =  "Combat/Enemy/Limb", order = 1)]
public class LimbScriptable : ScriptableObject
{
    [Range(0f, 100f)] public float Dodge; 
        
        
    public float  HP;

    [Range(0f, 1f)]
    public float BludgeoningResistance, SlashingResistance, StabbingResistance, DivineResistance, ElementResistance;

    public bool Critical;


 //   public List<CombatAbility> Abilities;


}
