using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public bool isEvent;
    
    private void OnTriggerEnter(Collider collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player && isEvent)
        {
            Interact(player);
        }
        else if (player && !isEvent ) 
        {
            player.Interactable = this;
        }
    }

     private void OnTriggerExit(Collider collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player  && player.Interactable == this)
        {
            player.Interactable = null;
        }
     }


   public virtual void Interact( Player player)
   {
        print("Interacted");

   }
}
