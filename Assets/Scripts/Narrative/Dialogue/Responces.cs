using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Responce Options", menuName = "Text Content/Responce Options", order = 1)]
public class Responces : ScriptableObject
{
    //Think of a way to do branching dialogue 
    //For now , might split this into further sets of scriptable objects that will have a "Next dialgoe in it"
    // this might get complex in code within dialogue manager

    public string Text;
    public ResponceOutcome Outcome;
    public Dialogue NextDialogue;


    // None for skip to next dialogue from speaker
    public enum ResponceOutcome
    {
        None,
        Continue,
        Exit,
        NewMenu,
        Fight,
        Rectruit,
        Loot,
        CrossSave
    }

    //Later will add code that will tick certain bools in a global event manager, or just do it within dialogue class


}
