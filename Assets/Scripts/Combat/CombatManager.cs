using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public CombatEncounter CurrentEncounter;

   

    
    public static CombatManager instance { get; private set; }
    
    
    
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        instance = this;
        
        
    }


}
