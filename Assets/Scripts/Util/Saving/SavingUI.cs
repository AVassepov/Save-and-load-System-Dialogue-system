using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingUI : MonoBehaviour
{
    [SerializeField] private string SaveName;


    public void SaveGame()
    {
        SaveSystem.Instance.CurrentSave.SaveName = SaveName;
        SaveSystem.Instance.SaveGame();
    }


    public void LoadGame()
    {
        
        SaveSystem.Instance.CurrentSave.SaveName = SaveName;
        SaveSystem.Instance.LoadGame();
    }   
    
}
