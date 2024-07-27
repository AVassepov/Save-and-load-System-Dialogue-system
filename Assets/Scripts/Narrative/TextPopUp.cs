using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPopUp : Interactable
{
    [SerializeField]private  Dialogue OpeningDialogue;
    private DialogueManager DialogueManager;


    private void Awake()
    {
        DialogueManager = FindAnyObjectByType<DialogueManager>();
    }


    public override void Interact(Player player)
    {
        base.Interact(player);

        if (DialogueManager.OpeningDialogue == null)
        {
            DialogueManager.OpeningDialogue = OpeningDialogue;
            DialogueManager.InitiateDialogue();
        } else if (DialogueManager.NextDialogue.NextDialogue == null && DialogueManager.NextDialogue.Responces.Count ==0) 
        {
            DialogueManager.FinishDialogue();
        }
        else
        {

            if (!DialogueManager.printing) { 
            DialogueManager.ContinueDialogue();
            }
            else
            {
                DialogueManager.SkipScroll();
            }
        }

    }





}
