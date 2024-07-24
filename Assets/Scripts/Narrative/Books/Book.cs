using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Book : Interactable
{

    [SerializeField] private Sprite BookImage;
    [SerializeField] private BookScriptable BookScriptable;
    private void Start()
    {
        Interact();
        print(  SoundLibrary.instance.BookSounds.Count);
    }

    public override void Interact()
    {
        base.Interact();
        BookUI.instance.StartReading(BookImage,BookScriptable);        
    }
    
}
