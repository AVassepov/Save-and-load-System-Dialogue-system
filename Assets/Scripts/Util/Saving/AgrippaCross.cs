
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AgrippaCross : Interactable
{



    public Outcomes Outcomes;
    public int ID;

    [SerializeField] private LootPool lootPool;
    [SerializeField] private Dialogue savingInitiation;

    public CrossData Data = new CrossData();
    private Character mainCharacter;

    private void Start()
    {
        Data.Loot=   LootSpawner.InitiateLoot(lootPool, Random.Range(1, 4));
        
        StatusCheck();
    }

   public void InitiateSave()
    {
        //save to save files 
        Data.alreadySaved = true;
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



        for (int i = 0; i < Data.Loot.Count; i++)
        {
            GameStateManager.Instance.UpdateInventory( Data.Loot[i]);
        }


    }

    public void StatusCheck()
    {
        GameState state = GameStateManager.Instance.CurrentGameState;
        int index = -1;
        for (int i = 0; i < state.Crosses.Count; i++)
        {
            if (state.Crosses[i].worldID == ID)
            {
                index = i;
            }
        }

        if (index != -1)
        {
           Data= state.Crosses[index];
           print("FOUND EXISTING CROSS INSTANE");
        }
        else
        {
            state.Crosses.Add(Data);
            print("First time loading cross");
        }
        
        GameStateManager.Instance.SaveState();
    }

    public Dialogue CheckSaveStatus()
    {
        CharacterStats stats = Player.Instance.GetComponent<Character>().CharacterData.CharacterStatistics;
        
        if (!Data.alreadySaved && !Data.alreadyRobbed && stats.Piety > -3 && stats.Iconoclaust<5 && stats.Heretic<3)
        {
            DialogueManager.Instance.SavingUI.SetActive(true);
            
            DialogueManager.Instance.WaitForDialogue = true;
            print(Outcomes.SuccessfulSave.Text);
            return Outcomes.SuccessfulSave;
        }else if (Data.alreadySaved )
        {
            print(Outcomes.RepeatedSave.Text);
            return Outcomes.RepeatedSave;
        }else if (Data.alreadyRobbed)
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



[Serializable]
public class CrossData
{
    public bool alreadySaved, alreadyRobbed;
    public List<Item> Loot;
    public int worldID;


}