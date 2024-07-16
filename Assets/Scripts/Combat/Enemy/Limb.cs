using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Limb", menuName =  "Combat/Enemy/Limb", order = 1)]
public class Limb : ScriptableObject
{
    public float Dodge, HP;

    [Range(0f, 1f)]
    public float BludgeoningResistance, SlashingResistance, StabbingResistance, DivineResistance, ElementResistance;

    public bool Critical;


 //   public List<CombatAbility> Abilities;


}
