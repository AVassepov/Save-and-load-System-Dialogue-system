using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "Description Text", menuName = "Text Content/Description Text", order = 1)]
public class DescriptionText: ScriptableObject
{
    public List<string> Texts;

    public List<string> Speakers;
}
