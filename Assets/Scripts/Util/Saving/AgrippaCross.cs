
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AgrippaCross : Interactable
{



    public Outcomes Outcomes;


    public bool Saved, Robbed;



    public List<Item> Items;
    [SerializeField] private LootPool lootPool;
    [SerializeField] private Dialogue savingInitiation;
    private Character mainCharacter;

    private void Awake()
    {
        LootSpawner.InitiateLoot(lootPool, Random.Range(1, 4));
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

        mainCharacter.CharacterData.CharacterStatistics.Heretic += 1;
        mainCharacter.CharacterData.CharacterStatistics.Iconoclaust += 1;
        mainCharacter.CharacterData.CharacterStatistics.Piety -= 2;


        AddLoot();


    }

    public override void Interact(Player player)
    {
        base.Interact(player);

        mainCharacter = player.GetComponent<Character>();

        DialogueManager.Instance.CurrentCross = this;
        DialogueManager.Instance.OpeningDialogue = savingInitiation;
        DialogueManager.Instance.InitiateDialogue();


    }

    void AddLoot()
    {



        for (int i = 0; i < Items.Count; i++)
        {
            GameStateManager.Instance.UpdateInventory(Items[i]);
        }


    }

    public Dialogue CheckSaveStatus()
    {
        CharacterStats stats = Player.Instance.GetComponent<Character>().CharacterData.CharacterStatistics;
        
        if (!Saved && !Robbed && stats.Piety > -3 && stats.Iconoclaust<5 && stats.Heretic<3)
        {
            print(Outcomes.SuccessfulSave.Text);
            return Outcomes.SuccessfulSave;
        }else if (Saved)
        {
            print(Outcomes.RepeatedSave.Text);
            return Outcomes.RepeatedSave;
        }else if (Robbed)
        {
            print(Outcomes.RobbedSave.Text);
            return Outcomes.RobbedSave;
        }
        else
        {
            print(Outcomes.FailedSave.Text);
            return Outcomes.FailedSave;
        }
    }

}





[Serializable]
public struct Outcomes
{
    public Dialogue SuccessfulSave;
    public Dialogue FailedSave;
    public Dialogue RobbedSave;
    public Dialogue RepeatedSave;

}
