using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    
    //saving for all of the enemies is done in a pool in a (enemy manager) class that stores a list of all enemies with their IDS and their respective conditions
    //or maybe inherit from character to create an Enemy class and manage those in a class still called (enemy manager)



    public CharacterData CharacterData;

    public FileName SavedFileName;

    public enum FileName
    {
        None,
        ManAtArms,
        Peasant,
        Brigand,
        Victim

    }

    public enum ArmorSlots {
        Head,
        Body,
        Feet,
        Trinket
    }

    public enum Buff {

        Strength,
        Clarity,
        Blessing,
        Faith
    }

    public enum DamageType{
        Slashing,
        Bludgeoning,
        Stabbing,
        Divine,
        Elemental
    }
    public enum Affliction{
        Bleed,
        Poison,
        Panic,
        Rot,
        Contusion,
        Curse,
        Infection,
        Parasites,
        Nausea,
        Fracture,
        Migrane

    }


    private void Awake()
    {
        SaveCharacter();
    }

    public void UpdateEquipment(Item NewEquipment)
    {
        Weapon weapon = NewEquipment as Weapon;
        Armor armor = NewEquipment as Armor;

        if (armor)
        {
            print("was armor");
        }
        else if (weapon)
        {
            print("was weapon");
        }
        else
        {
            print("was other");
        }
    }


    public void UpdateResources(float health , float food, float will)
    {
        CharacterData.CharacterConditions.Health += health;
        CharacterData.CharacterConditions.Hunger += food;
        CharacterData.CharacterConditions.Will += will;
    }


    public void SaveCharacter()
    {
        string character = JsonUtility.ToJson(CharacterData);
        string path = Application.persistentDataPath + "/" + SavedFileName.ToString() + ".json";

        Debug.Log(path);
        System.IO.File.WriteAllText(path, character);
    }

    public void LoadCharacter()
    {
        string path = Application.persistentDataPath + "/" + SavedFileName.ToString() + ".json";
        string character = System.IO.File.ReadAllText(path);

        CharacterData = JsonUtility.FromJson<CharacterData>(character);
    }
}



[System.Serializable]
public class Equipment
{
    public Armor Helmet;
    public Armor Torso;
    public Armor Boots;
    public Armor Trinket1, Trinket2;
    public Weapon Weapon;


}
[System.Serializable]
public class CharacterStats
{
    public float SlashRes, PierceRes, BludgeonRes, DivineRes, ElementalRes;
    public float Strength;
    public float Innitiative;
    public float CastingStrength;

    public int Piety, Iconoclaust, Heretic;


}

[System.Serializable]
public class Conditions
{
    public float Health=100, Hunger=100,Will =100;
    public int Legs=2 , Arms =2;
    public List<Character.Affliction> Afflictions;
    public List<Character.Buff> Buffs;
}

[System.Serializable]
public class CharacterData
{
    public Equipment CharacterEquipment;
    public Conditions CharacterConditions;
    public CharacterStats CharacterStatistics;
}