using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dice : MonoBehaviour
{

    private Rigidbody rb;

    private void Update()
    {



        if (Input.GetKeyDown(KeyCode.Space))
        {
            ThrowDice();
        }
        
        
        if (rb && rb.velocity == Vector3.zero)
        {
            print(GetResult());
            Destroy(this);
        }
    }   


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ThrowDice();
    }

    void ThrowDice()
    {
       // transform.Rotate( new Vector3(Random.Range(0,360f),Random.Range(0,360f),Random.Range(0,360f)));

       rb.angularVelocity = new Vector3(Random.Range(3f,10f),Random.Range(3f,10f),Random.Range(3f,10f)) ;
        
    }



    public int GetResult()
    {
            //check transform right , forward , up
            //depending on which one has a 1 or -1  we get a result

            float check = transform.up.y;

            if (check >= 0.999f)
            {
                return 1;
            }else if (check <=-0.999f)
            {
                return 6;
            }

            check = transform.forward.y;
            
            if (check >= 0.999f)
            {
                return 5;
            }else if (check <=-0.999f)
            {
                return 2;
            }
            
            check = transform.right.y;
            
            if (check >= 0.999f)
            {
                return 3;
            }else if (check <=-0.999f)
            {
                return 4;
            }



        return 0;
    }
}
