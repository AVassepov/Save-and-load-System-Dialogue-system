using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public InventoryMenu InventoryMenu;
    public Item Item;
    private TextMeshProUGUI TextName;


    private void Start()
    {
        TextName = GetComponentInChildren<TextMeshProUGUI>();
        TextName.text = Item.name;
    }
    public void Select()
    {
        print("SELECTED : " + Item.name);
    }

}
