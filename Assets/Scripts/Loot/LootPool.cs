using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Loot Pool", menuName = "Items/Loot Pool", order = 4)]
public class LootPool : ScriptableObject
{
    public Vector3 Chances = new Vector3(6,3,1);


    public List<Item> Common;
    public List<Item> Rare;
    public List<Item> Legendary;
}
