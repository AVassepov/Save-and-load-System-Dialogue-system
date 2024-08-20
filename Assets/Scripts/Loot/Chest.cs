using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    [SerializeField] private LootPool pool;
    private DialogueManager manager;
    //later add sounds here and perhaps animation , also destroy after some time and not instantly for those cases
    //but also lock out 

    private void Awake()
    {
        manager = FindObjectOfType<DialogueManager>();
  

    }

    public override void Interact(Player player)
    {
        base.Interact(player);
        Item item = LootSpawner.InitiateLoot(pool, 1)[0];
        manager.Notification(item.name , 3);
       GameStateManager.Instance.UpdateInventory(item);
        
        Destroy(this);
    }


}
