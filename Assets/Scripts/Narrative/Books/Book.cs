using UnityEngine;

public class Book : Interactable
{
    [SerializeField] private SoundLibrary sounds;
    [SerializeField] private Sprite BookImage;
    [SerializeField] private BookScriptable BookScriptable;
    
    
    
    
    public override void Interact(Player player)
    {
        base.Interact(player);
        BookUI.instance.StartReading(BookImage,BookScriptable ,sounds );        
    }
    
}
