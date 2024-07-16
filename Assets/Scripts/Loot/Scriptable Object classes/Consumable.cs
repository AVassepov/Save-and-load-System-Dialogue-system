using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Consumable", menuName = "Items/Consumable", order = 1)]
public class Consumable : Item
{
    public List<Character.Affliction> Cures;
    public List<Character.Affliction> Afflictions;
    public List<Character.Buff> Buffs;

    public int Food, Will;

}
