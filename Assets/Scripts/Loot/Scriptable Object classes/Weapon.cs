using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapons", order = 1)]
public class Weapon : Item
{
    public float BaseDamage;

    public float DamageVariance;

    public Character.DamageType Type;
}
