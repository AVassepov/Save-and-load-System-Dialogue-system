using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPopUp : Interactable
{
    [SerializeField]private  Dialogue OpeningDialogue;
    private DialogueManager DialogueManager;


    private void Start()
    {
        DialogueManager = DialogueManager.Instance;
    }


    public override void Interact(Player player)
    {
        base.Interact(player);

        if (DialogueManager.OpeningDialogue == null)
        {
            DialogueManager.OpeningDialogue = OpeningDialogue;
            DialogueManager.InitiateDialogue();
            print("Started dialogue");
        } else if (DialogueManager.NextDialogue==null ||(DialogueManager.NextDialogue.NextDialogue == null && DialogueManager.NextDialogue.Responces.Count ==0)) 
        {
            DialogueManager.FinishDialogue();
        }
        
        /*
        else
        {

            if (!DialogueManager.printing) { 
            DialogueManager.ContinueDialogue();
            }
            else
            {
                DialogueManager.SkipScroll();
            }
        }*/

    }





}
