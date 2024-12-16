using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    
    //saving for all of the enemies is done in a pool in a (enemy manager) class that stores a list of all enemies with their IDS and their respective conditions
    //or maybe inherit from character to create an Enemy class and manage those in a class still called (enemy manager)

    
    private float timePassed;
    public int HungerDelay = 20;

    
    
    [SerializeField] Armor EquipThis;
    public bool PlayerCharacter;

    public CharacterData CharacterData;
    private bool loaded;
   // public FileName SavedFileName;

    
    /*public enum FileName
    {
        None,
        ManAtArms,
        Peasant,
        Brigand,
        Victim

    }*/

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

    private void Start()
    {
        
        //something here makes all characters in combat have the same stats
        
        //add this to combat if its a player or a teammate 
        if (CombatManager.Instance != null && !CombatManager.Instance.PlayerCharacters.Contains(CharacterData) && PlayerCharacter)
        {
            CombatManager.Instance.PlayerCharacters.Add(CharacterData);
        }
        
        
        if(GameStateManager.Instance && GameStateManager.Instance.CurrentGameState.Characters.Count> 0){
            for (int i = 0; i < GameStateManager.Instance.CurrentGameState.Characters.Count; i++)
            {
                if (GameStateManager.Instance.CurrentGameState.Characters[i].CharacterName == CharacterData.CharacterName)
                {
                    //  CharacterData = GameStateManager.Instance.CurrentGameState.Characters[i];
                    loaded = true; 
                    ResetResistances();
                }
            }
        }

        //add this to all characters in manager
        if (GameStateManager.Instance  &&!loaded)
        {
            GameStateManager.Instance.CurrentGameState.Characters.Add(CharacterData);
        }
    }

    private void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed>HungerDelay)
        {
            print("hungry");
            HungerCheck();
            timePassed = 0;
        }
       
    }


    public void Die(){
        
    }



    public void UpdateResistances(Armor armor, Armor oldArmor)
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


    /*public void SaveCharacter()
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
    }*/

    private void ResetResistances()
    {
        CharacterData.CharacterStatistics.BludgeonRes = 0;
        CharacterData.CharacterStatistics.DivineRes = 0;
        CharacterData.CharacterStatistics.ElementalRes = 0;
        CharacterData.CharacterStatistics.SlashRes = 0;
        CharacterData.CharacterStatistics.BludgeonRes = 0;
        if (CharacterData.CharacterEquipment.Helmet >= 0)
        {
            UpdateResistances(ItemLibrary.Instance.Armors[CharacterData.CharacterEquipment.Helmet],null);
        }

        if (CharacterData.CharacterEquipment.Torso >= 0)
        {
            UpdateResistances(ItemLibrary.Instance.Armors[CharacterData.CharacterEquipment.Torso],null);
        }

        if (CharacterData.CharacterEquipment.Boots >= 0)
        {
            UpdateResistances(ItemLibrary.Instance.Armors[CharacterData.CharacterEquipment.Boots],null);
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

    
    public void UpdateEquipment(Item NewEquipment)
    {
        Weapon weapon = NewEquipment as Weapon;
        Armor armor = NewEquipment as Armor;

        if (armor)
        {
            Armor oldArmor = null;


            if (armor.Slot == ArmorSlots.Body && CharacterData.CharacterEquipment.Torso >= 0)
            {
                oldArmor = ItemLibrary.Instance.Armors[CharacterData.CharacterEquipment.Torso];
                CharacterData.CharacterEquipment.Helmet = ItemLibrary.Instance.Armors.IndexOf(armor);
            }
            else if (armor.Slot == ArmorSlots.Head && CharacterData.CharacterEquipment.Helmet >= 0)
            {
                oldArmor = ItemLibrary.Instance.Armors[CharacterData.CharacterEquipment.Helmet];
                CharacterData.CharacterEquipment.Helmet = ItemLibrary.Instance.Armors.IndexOf(armor);
            }
            else if (armor.Slot == ArmorSlots.Feet && CharacterData.CharacterEquipment.Boots >= 0)
            {
                oldArmor = ItemLibrary.Instance.Armors[CharacterData.CharacterEquipment.Boots];
                CharacterData.CharacterEquipment.Helmet = ItemLibrary.Instance.Armors.IndexOf(armor);
            }

            UpdateResistances(armor, oldArmor);


            print("was armor");
        }
        else if (weapon)
        {
            CharacterData.CharacterEquipment.Weapon = ItemLibrary.Instance.Weapons.IndexOf(weapon);
            print("was weapon");
        }
        else
        {
            print("was other");
        }
        
    }
    
    
    public void SetInnitiative(int newInnitiative)
    {
        CharacterData.CharacterStatistics.Innitiative = newInnitiative;
        print("Updated innitiative = " + newInnitiative);
    }
}




[System.Serializable]
public class CharacterStats
{
    public float SlashRes, PierceRes, BludgeonRes, DivineRes, ElementalRes;
    public float Strength;
    public int Innitiative;
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

    public string CharacterName;
}

[System.Serializable]
public class Equipment
{
    public int Helmet = -1;
    public int Torso = -1;
    public int Boots = -1;
    public int Trinket1 = -1, Trinket2 = -1;
    public int Weapon = -1;

    public int CharacterModel = 0;
}