using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Vector3> DefaultPositions;

    public List<GameObject> Enemies;

    private List<Enemy> enemyInstances= new List<Enemy>();
    public List<string> ID;


    public static EnemySpawner Instance { get; private set; }       

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        Instance = this;

    }

    void Start()
    {
        SpawnEnemies();
        CheckExistance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void SpawnEnemies()
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemy enemy = Instantiate(Enemies[i], DefaultPositions[i], Quaternion.identity).GetComponent<Enemy>();
            enemyInstances.Add(enemy);
            enemy.UnitData.ID = ID[i];
        }
    }

    private void CheckExistance()
    {
        UnitTracker state = GameStateManager.Instance.CurrentGameState.Units;
        if (enemyInstances.Count > 0)
        {
            for (int i = 0; i < enemyInstances.Count; i++)
            {
                int index = state.CheckID(enemyInstances[i].UnitData.ID, state.UnitDatas);
                //check and set position
                if (index!=-1 )
                {
                    enemyInstances[i].UnitData = state.UnitDatas[index];
                    enemyInstances[i].transform.position = enemyInstances[i].UnitData.Position;
                    enemyInstances[i].Alive = enemyInstances[i].UnitData.Alive;
                    /*enemyInstances[i].transform.position = state.Positions[state.ID.IndexOf(enemyInstances[i].ID)];
                    enemyInstances[i].Alive = state.Alive[state.ID.IndexOf(enemyInstances[i].ID)];*/
                }
                else
                {
                    //if first time add to lists
                    state.UnitDatas.Add(enemyInstances[i].UnitData);
                }
                
                
                
                
                
            }
        }

    }

    public void SavePositions()
    {
        UnitTracker state = GameStateManager.Instance.CurrentGameState.Units;
        if ( enemyInstances.Count > 0)
        {
            for (int i = 0; i < enemyInstances.Count; i++)
            {
                int index = state.CheckID(enemyInstances[i].UnitData.ID, state.UnitDatas);
                if (state!=null && index!=-1)
                {
                    state.UnitDatas[index].Position = enemyInstances[i].transform.position;
                    state.UnitDatas[index].Alive = enemyInstances[i].Alive;
                }
            }
        }

        GameStateManager.Instance.SaveState();
        print("saved positions");
    }
}
