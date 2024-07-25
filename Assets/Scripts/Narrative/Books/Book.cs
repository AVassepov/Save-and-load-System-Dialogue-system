using UnityEngine;

public class Book : Interactable
{

    [SerializeField] private Sprite BookImage;
    [SerializeField] private BookScriptable BookScriptable;
    
    
    
    
    public override void Interact()
    {
        base.Interact();
        BookUI.instance.StartReading(BookImage,BookScriptable);        
    }
    
}
