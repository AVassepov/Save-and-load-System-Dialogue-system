using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSystem : MonoBehaviour
{
    public Save CurrentSave;

    public static SaveSystem Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        Instance = this;
        
        DontDestroyOnLoad(this);
    }

    public void SaveGame()
    {
        CurrentSave.State = GameStateManager.Instance.CurrentGameState;
        CurrentSave.Location = SceneManager.GetActiveScene().name;
        
        string save = JsonUtility.ToJson(CurrentSave);
        string path = Application.persistentDataPath +  "/" + CurrentSave.SaveName +".json";

        Debug.Log(path);
        System.IO.File.WriteAllText(path, save);
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/" + CurrentSave.SaveName +".json";
        string save = System.IO.File.ReadAllText(path);

        CurrentSave = JsonUtility.FromJson<Save>(save);
        GameStateManager.Instance.SaveName = CurrentSave.SaveName;
        GameStateManager.Instance.CurrentGameState = CurrentSave.State;

    }









}

[System.Serializable]
public class Save
{
    public GameState State;
    public string SaveName;
    public string Location;
    public Image ScreenCap;



}
