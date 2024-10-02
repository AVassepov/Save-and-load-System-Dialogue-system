using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{
    public CombatEncounter CurrentEncounter;
    public UnitData Data;


    public List<EnemyCombat> EnemyInstances;
    
    public List<Character> Combatants;
    
    public static CombatManager instance { get; private set; }
    
    
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        instance = this;
        SceneManager.sceneLoaded += OnSceneLoaded;
       DontDestroyOnLoad(gameObject);
        
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

        if (scene.name == "Combat")
        {
            SpawnEnemies();
            GetEnemyLimbs();
            SetInitiative();
            print("Started fight");
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
        Combatants = new List<Character>();
        Combatants.AddRange(GetComponents<PlayerCharacter>());
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
