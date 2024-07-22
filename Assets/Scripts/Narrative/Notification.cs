using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : Interactable
{
    private DialogueManager manager;

    [SerializeField] private string Text;
    private void Awake()
    {
        isEvent = true;
        manager = FindObjectOfType<DialogueManager>();
    }

    public override void Interact()
    {
        base.Interact();

        manager.Notification(Text, 5);
        Destroy(gameObject);


    }
}