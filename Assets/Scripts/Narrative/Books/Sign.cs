using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : Interactable
{
    [SerializeField] private SoundLibrary sounds;
    [SerializeField] private Sprite signSprite;
    public string SignText;
    
    
    public override void Interact(Player player)
    {
        base.Interact(player);
        BookUI.instance.ReadSign(signSprite,SignText , sounds);        
    }

    
}
