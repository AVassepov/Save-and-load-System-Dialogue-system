using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public Inventory CurrentInventory = new Inventory();

    public void SaveIntoJson()
    {

        string inventory = JsonUtility.ToJson(CurrentInventory);
        string path =  Application.persistentDataPath + "/InventoryData.json";

        Debug.Log(path);
        System.IO.File.WriteAllText(path, inventory);

    }

    public void LoadFromJson()
    {
        string path = Application.persistentDataPath + "/InventoryData.json";
        string inventory = System.IO.File.ReadAllText(path);

        CurrentInventory = JsonUtility.FromJson<Inventory>(inventory); 
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SaveIntoJson();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            LoadFromJson();
        }
    }

    public void UpdateInventory(Item AddThis)
    {
        if (AddThis is Armor)
        {
            CurrentInventory.Armors.Add(ItemLibrary.instance.Armors.IndexOf(AddThis as Armor));
        }
        else if (AddThis is Weapon)
        {
            CurrentInventory.Weapons.Add(ItemLibrary.instance.Weapons.IndexOf(AddThis as Weapon));
        }
        else if (AddThis is Consumable)
        {
            CurrentInventory.Consumables.Add(ItemLibrary.instance.Consumables.IndexOf(AddThis as Consumable));
        }
        else
        {
            CurrentInventory.KeyItems.Add(ItemLibrary.instance.KeyItems.IndexOf(AddThis as KeyItem));
        }

        SaveIntoJson();
    }
}


[System.Serializable]
public class Inventory
{

    public List<int> Armors;
    public List<int> Weapons;
    public List<int> Consumables;
    public List<int> KeyItems;
    public int Money;





}
