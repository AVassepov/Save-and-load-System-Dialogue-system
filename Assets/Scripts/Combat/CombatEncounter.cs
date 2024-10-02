using UnityEngine;

[CreateAssetMenu(fileName = "Encounter", menuName = "Combat/Encounter", order = 1)]
public class CombatEncounter : ScriptableObject
{
    public string Name;
    public GameObject[] Enemies;
    
}
