using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }


    [SerializeField]public GameState CurrentGameState;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        Instance = this;
        
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
        string path = Application.persistentDataPath + "/" + "GameState.json";

        Debug.Log(path);
        System.IO.File.WriteAllText(path, state);
    }

    public void LoadState()
    {
        string path = Application.persistentDataPath + "/" + "GameState.json";
        string state = System.IO.File.ReadAllText(path);

        CurrentGameState = JsonUtility.FromJson<GameState>(state);
    }
    
    
    
}



[System.Serializable]
public class GameState
{
    public List<CharacterData> Characters = new List<CharacterData>();
    public UnitTracker Units = new UnitTracker();
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
