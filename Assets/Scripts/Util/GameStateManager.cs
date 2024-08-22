using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }


    public string SaveName;
    
    [SerializeField]public GameState CurrentGameState;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        Instance = this;

        SaveName = SaveSystem.Instance.CurrentSave.SaveName;
        
        LoadState();
    }
    
    public void Update(){
        if (Input.GetKeyDown(KeyCode.T))
        {
            SaveState();
            print("Saved");
        }else  if (Input.GetKeyDown(KeyCode.Y))
        {
            LoadState();
            print("Load");
        }else  if (Input.GetKeyDown(KeyCode.R))
        {
            CurrentGameState = new GameState();
            SaveState();
            print("Reset");
        }
    }
    
    public void SaveState()
    {
        string state = JsonUtility.ToJson(CurrentGameState);
        string path = Application.persistentDataPath +  "/" + SaveName+"_state" +".json";

        Debug.Log(path);
        System.IO.File.WriteAllText(path, state);
    }

    public void LoadState()
    {
        string path = Application.persistentDataPath + "/" + SaveName+"_state" +".json";
        string state = System.IO.File.ReadAllText(path);

        CurrentGameState = JsonUtility.FromJson<GameState>(state);
    }
    
    
    
    public void UpdateInventory(Item AddThis)
    {
        if (AddThis is Armor)
        {
           CurrentGameState.CurrentInventory.Armors.Add(ItemLibrary.Instance.Armors.IndexOf(AddThis as Armor));
        }
        else if (AddThis is Weapon)
        {
            CurrentGameState.CurrentInventory.Weapons.Add(ItemLibrary.Instance.Weapons.IndexOf(AddThis as Weapon));
        }
        else if (AddThis is Consumable)
        {
            CurrentGameState.CurrentInventory.Consumables.Add(ItemLibrary.Instance.Consumables.IndexOf(AddThis as Consumable));
        }
        else
        {
            CurrentGameState.CurrentInventory.KeyItems.Add(ItemLibrary.Instance.KeyItems.IndexOf(AddThis as KeyItem));
        }

        SaveState();
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




[System.Serializable]
public class GameState
{
    public List<CharacterData> Characters = new List<CharacterData>();
    public UnitTracker Units = new UnitTracker();
    public Inventory CurrentInventory = new Inventory();
}

[System.Serializable]
public class UnitTracker
{
    public List<UnitData> UnitDatas = new List<UnitData>();


    public  int CheckID(string ID, List<UnitData> UnitDatas)
    {
        for (int i = 0; i < UnitDatas.Count; i++)
        {
            if (UnitDatas[i].ID == ID)
            {
                return i;
            }
        }

        return -1;
    }
}
