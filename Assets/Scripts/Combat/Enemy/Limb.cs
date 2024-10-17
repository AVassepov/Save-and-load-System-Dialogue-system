using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limb : Character
{

    public float Health;
    public bool Critical;
    public EnemyCombat Enemy;





    public List<EnemyMoveScriptable> MoveList;
    public void DealDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            //destroy limb

            if (Critical)
            {
                Enemy.Die();
            }
            else
            {
                LoseLimb();
            }
        }


    }

    public void LoseLimb()
    {
        gameObject.SetActive(false);
        
    }

}
