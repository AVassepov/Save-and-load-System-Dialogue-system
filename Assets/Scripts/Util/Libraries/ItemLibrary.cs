using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLibrary : MonoBehaviour
{
    public static ItemLibrary Instance { get; private set; }
    
    
    public List<Armor> Armors;
    public List<Weapon> Weapons;
    public List<Consumable> Consumables;
    public List<KeyItem> KeyItems;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        Instance = this;

    }

}
