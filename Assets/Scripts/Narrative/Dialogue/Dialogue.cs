using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Text Content/Dialogue", order = 1)]
public class Dialogue : ScriptableObject
{
    public string Text ="";
    public string Speaker ="";

    public Dialogue NextDialogue;

    public List<Responces> Responces;
    
}
