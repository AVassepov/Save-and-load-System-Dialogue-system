using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{
    public CombatEncounter CurrentEncounter;
    public UnitData Data;


    public List<Character> Players;
    public List<CharacterData> PlayerCharacters = new List<CharacterData>();

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

        if (scene.name == "Combat")
        {
            SpawnEnemies();
            GetEnemyLimbs();
            GetPlayerCharacter();
            Combatants.AddRange(Players);
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
                    innitiatives.Insert(j, currentInitiative);


                    // same logic but for rearanging the actual unit order
                    Character currentCharacter = temp[i];
                    temp.RemoveAt(i);
                    temp.Insert(j, currentCharacter);
                }
            }
        }


        Combatants = temp;
        for (int i = 0; i < Combatants.Count; i++)
        {
            Combatants[i].SetInnitiative(innitiatives[i]);
        }
    }


    public void GetPlayerCharacter()
    {
        for (int i = 0; i < PlayerCharacters.Count; i++)
        {
            GameObject temp =
                Instantiate(ItemLibrary.Instance.CharacterModels[PlayerCharacters[i].CharacterEquipment.CharacterModel],
                    new Vector3(0, 0, i * 2), Quaternion.identity);
            //PlayerCharacters.Add(temp.GetComponent<Character>().CharacterData);
            print(temp.name);
            Players.AddRange(FindObjectsOfType<PlayerCombat>());
        }


        /*for (int i = 0; i < FindObjectsOfType<PlayerCombat>().Length; i++)
        {
            if (PlayerCharacters.Count <= i)
            {
                if(FindObjectsOfType<PlayerCombat>().Length >= i){
                    FindObjectsOfType<PlayerCombat>()[i].CharacterData = PlayerCharacters[i];
                }
            }
        }*/
    }

    public void SpawnEnemies()
    {
        if (CurrentEncounter)
        {
            for (int i = 0; i < CurrentEncounter.Enemies.Length; i++)
            {
                EnemyInstances.Add(
                    Instantiate(CurrentEncounter.Enemies[i], new Vector3(0, i * 2, 0), Quaternion.identity)
                        .GetComponent<EnemyCombat>());
            }
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
