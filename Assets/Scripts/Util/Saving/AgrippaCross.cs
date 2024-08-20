
using System.Collections.Generic;
using UnityEngine;
public class AgrippaCross : Interactable
{




    public List<Item> Items;
    [SerializeField] private LootPool lootPool;

    private Character mainCharacter;
    
    private void Awake()
    {
        LootSpawner.InitiateLoot( lootPool, Random.Range(1, 4) );
    }

    void InnitiateSave()
    {
        //save to save files 
        
        mainCharacter.CharacterData.CharacterStatistics.Heretic -= 2;
        mainCharacter.CharacterData.CharacterStatistics.Piety += 3;


    }


    void Desecrate()
    {
        // Grant the loot and change scores

        mainCharacter.CharacterData.CharacterStatistics.Heretic +=1;
        mainCharacter.CharacterData.CharacterStatistics.Iconoclaust += 1;
        mainCharacter.CharacterData.CharacterStatistics.Piety -= 2;


        AddLoot();


    }
    
    public override void Interact(Player player)
    {
        base.Interact(player);
        
        print("pray lil bro");
        mainCharacter = player.GetComponent<Character>();

    }

    void AddLoot()
    {
        


        for (int i = 0; i < Items.Count; i++)
        {
            GameStateManager.Instance.UpdateInventory(Items[i]);
        }


    }    
    
}
