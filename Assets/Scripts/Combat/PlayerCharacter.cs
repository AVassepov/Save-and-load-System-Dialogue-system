using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    
    private float timePassed;
    public int HungerDelay = 20;

    
    
    [SerializeField] Armor EquipThis;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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

}
[System.Serializable]
public class Equipment
{
    public int Helmet=-1;
    public int Torso=-1;
    public int Boots=-1;
    public int Trinket1=-1, Trinket2=-1;
    public int Weapon=-1;


}