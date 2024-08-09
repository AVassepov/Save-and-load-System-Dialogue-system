using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance { get; private set; }


    public GameState CurrentGameState;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }

        instance = this;
        
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
    private List<CharacterData> Characters;
}