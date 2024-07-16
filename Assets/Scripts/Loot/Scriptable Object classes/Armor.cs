using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Armor", menuName = "Items/Armor", order = 1)]
public class Armor : Item
{
    [Range(0f, 1f)]
    public float BludgeoningResistance, SlashingResistance, StabbingResistance, DivineResistance, ElementResistance;


    public Character.ArmorSlots Slot;
    
}
