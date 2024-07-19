using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
   public EnemySO EnemyScriptable;
   public float Initiative;
   public string Name;
   public List<LimbScriptable> Limbs;



   public void SetData()
   {
      Initiative = EnemyScriptable.Initiative;
      Limbs = EnemyScriptable.Limbs;
   }


   public void Die()
   {
      //combat manager end combat
      // tell overworld enemy to die too
   }


}
