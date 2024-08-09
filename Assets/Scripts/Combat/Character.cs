using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    
    //saving for all of the enemies is done in a pool in a (enemy manager) class that stores a list of all enemies with their IDS and their respective conditions
    //or maybe inherit from character to create an Enemy class and manage those in a class still called (enemy manager)


    private float timePassed;
    public CharacterData CharacterData;

    public FileName SavedFileName;

    public int HungerDelay = 20;
    
    [SerializeField] Armor EquipThis;
    
    
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
        Migrane,
        Weakness,
        Starvation

    }


    private void Awake()
    {
        LoadCharacter();
    }


    public void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed>HungerDelay)
        {
            print("hungry");
            HungerCheck();
            timePassed = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateEquipment(EquipThis);
        }else  if (Input.GetKeyDown(KeyCode.T))
        {
            SaveCharacter();
        }else  if (Input.GetKeyDown(KeyCode.Y))
        {
            LoadCharacter();
        }
        
        
    }

    public void HungerCheck()
    {
        CharacterData.CharacterConditions.Hunger--;

        if (CharacterData.CharacterConditions.Hunger == 0)
        {
          Die();  
        }else if (CharacterData.CharacterConditions.Hunger == 60)
        {
            CharacterData.CharacterConditions.Afflictions.Add(Affliction.Weakness);
        }else if (CharacterData.CharacterConditions.Hunger ==30)
        {
            CharacterData.CharacterConditions.Afflictions.Add(Affliction.Starvation);
        }
    }
    
    public void Die(){
        
    }


    public void UpdateEquipment(Item NewEquipment)
    {
        Weapon weapon = NewEquipment as Weapon;
        Armor armor = NewEquipment as Armor;

        if (armor)
        {
            Armor oldArmor = null;


            if (armor.Slot == ArmorSlots.Body)
            {
                oldArmor = CharacterData.CharacterEquipment.Torso;
                CharacterData.CharacterEquipment.Torso = armor;
            }
            else if (armor.Slot == ArmorSlots.Head)
            {
                oldArmor = CharacterData.CharacterEquipment.Helmet;
                CharacterData.CharacterEquipment.Helmet = armor;
            }
            else if (armor.Slot == ArmorSlots.Feet)
            {
                oldArmor = CharacterData.CharacterEquipment.Boots;
                CharacterData.CharacterEquipment.Boots = armor;
            }

            UpdateResistances(armor, oldArmor);
        

            print("was armor");
        }
        else if (weapon)
        {
            CharacterData.CharacterEquipment.Weapon = weapon;
            print("was weapon");
        }
        else
        {
            print("was other");
        }
    }


    void UpdateResistances(Armor armor, Armor oldArmor)
    {
        if (armor)
        {
            CharacterData.CharacterStatistics.BludgeonRes += armor.BludgeoningResistance;
            CharacterData.CharacterStatistics.SlashRes += armor.SlashingResistance;
            CharacterData.CharacterStatistics.PierceRes += armor.StabbingResistance;
            CharacterData.CharacterStatistics.ElementalRes += armor.ElementResistance;
            CharacterData.CharacterStatistics.DivineRes += armor.DivineResistance;
        }

        if (oldArmor)
        {
            CharacterData.CharacterStatistics.BludgeonRes -= oldArmor.BludgeoningResistance;
            CharacterData.CharacterStatistics.SlashRes -= oldArmor.SlashingResistance;
            CharacterData.CharacterStatistics.PierceRes -= oldArmor.StabbingResistance;
            CharacterData.CharacterStatistics.ElementalRes -= oldArmor.ElementResistance;
            CharacterData.CharacterStatistics.DivineRes -= oldArmor.DivineResistance;
        }
    }
    
    
    public void UpdateResources(float health, float food, float will)
    {
        
        // restore hp/wp/hunger granted by the consumable
        CharacterData.CharacterConditions.Health += health;
        CharacterData.CharacterConditions.Hunger += food;
        CharacterData.CharacterConditions.Will += will;

        //if ate and went above theresholds , cure ailments
        if (CharacterData.CharacterConditions.Hunger > 60)
        {
            if (CharacterData.CharacterConditions.Afflictions.Contains(Affliction.Weakness))
            {
                CharacterData.CharacterConditions.Afflictions.Remove(Affliction.Weakness);
            }
        }
        else if (CharacterData.CharacterConditions.Hunger > 30)
        {
            if (CharacterData.CharacterConditions.Afflictions.Contains(Affliction.Starvation))
            {
                CharacterData.CharacterConditions.Afflictions.Remove(Affliction.Starvation);
            }
        }
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