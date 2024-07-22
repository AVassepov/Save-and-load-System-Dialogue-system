using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Book", menuName = "Items/Books", order = 1)]
public class BookScriptable : KeyItem
{
    public List<string> Pages;
    public string Title;
}
