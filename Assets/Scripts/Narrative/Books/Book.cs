using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Book : Interactable
{

    [SerializeField] private Sprite bookImage;

    private void Start()
    {
        Interact();
    }

    public override void Interact()
    {
        base.Interact();
        BookUI.instance.StartReading(bookImage,null);        
    }
    
}
