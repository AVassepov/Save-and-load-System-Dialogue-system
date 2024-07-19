using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limb : MonoBehaviour
{

    public float Health;
    public bool critical;
    public EnemyCombat Enemy;
    
    public void DealDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            //destroy limb

            if (critical)
            {
                Enemy.Die();
            }
        }


    }



}
