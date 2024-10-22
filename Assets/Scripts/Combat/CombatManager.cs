using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{
    public CombatEncounter CurrentEncounter;
    public UnitData Data;


    public List<Character> Players = new List<Character>();
    public List<CharacterData> PlayerCharacters =  new List<CharacterData>(); 

    public List<EnemyCombat> EnemyInstances;
    
    public List<Character> Combatants;
    
    public static CombatManager Instance { get; private set; }
    
    
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
        SceneManager.sceneLoaded += OnSceneLoaded;
       DontDestroyOnLoad(gameObject);


       print(Instance);
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

        /*if (scene.name == "Combat")
        {
            SpawnEnemies();
            GetEnemyLimbs();
            GetPlayerCharacter();
            SetInitiative();
            print("Started fight");
        }*/
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnEnemies();
            GetEnemyLimbs();
            GetPlayerCharacter();
            SetInitiative();
        }
    }

    private void SetInitiative()
    {
        List<Character> temp = Combatants;
        List<int> innitiatives = new List<int>();

        for (int i = 0; i < temp.Count; i++)
        {
            innitiatives.Add(temp[i].CharacterData.CharacterStatistics.Innitiative);
        }
        
        
        
        for (int i = 0; i < Combatants.Count; i++)
        {
            for (int j = 0; j < innitiatives.Count; j++)
            {
                if (innitiatives[i] < innitiatives[j])
                {
                    
                    //this works for sorting , after this do a list sorting of associated combatanats as well
                    int currentInitiative = innitiatives[i];
                    innitiatives.RemoveAt(i);
                    innitiatives.Insert(j,currentInitiative);
                    
                    
                    // same logic but for rearanging the actual unit order
                    Character currentCharacter = temp[i];
                    temp.RemoveAt(i);
                    temp.Insert(j, currentCharacter);

                }   
            }
        }

        for (int i = 0; i < innitiatives.Count; i++)
        {
            print(innitiatives[i]);
        }

        Combatants = temp;
    }


    public void GetPlayerCharacter()
    {
        for (int i = 0; i < PlayerCharacters.Count; i++)
        {
            GameObject temp = Instantiate(ItemLibrary.Instance.CharacterModels[PlayerCharacters[i].CharacterEquipment.CharacterModel],
                    new Vector3(0, 0, i * 2), Quaternion.identity);
            temp.GetComponent<Character>().CharacterData = PlayerCharacters[i];

        }
        
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < CurrentEncounter.Enemies.Length; i++)
        {
          EnemyInstances.Add(  Instantiate(CurrentEncounter.Enemies[i], new Vector3(0,i*2,0), Quaternion.identity).GetComponent<EnemyCombat>());
        }
        
        
        
    }
    
    public void GetEnemyLimbs()
    {

        for (int i = 0; i < EnemyInstances.Count; i++)
        {
            Combatants.AddRange(EnemyInstances[i].Limbs);
        }
        
    }

    

}
