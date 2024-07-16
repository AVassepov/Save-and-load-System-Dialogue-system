using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenuTab : MonoBehaviour
{
    

    public  void SetCurrent()
    {
        transform.SetSiblingIndex(transform.parent.childCount);
        print("selected");
    }
}
