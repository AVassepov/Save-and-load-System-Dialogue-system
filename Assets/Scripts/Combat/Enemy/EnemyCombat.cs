using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
   
   public float Initiative;
   public string Name;
   public List<Limb> Limbs;

   private UnitData data;


   public void Awake()
   {
      data =CombatManager.Instance.Data;
      


   }  

   public void SetData()
   {

      for (int i = 0; i < Limbs.Count; i++)
      {
         Limbs[i].DealDamage( Limbs[i].Health -data.LimbHeath[i] );
      }
      
   }


   public void Die()
   {
      //combat manager end combat
      // tell overworld enemy to die too
   }


}
