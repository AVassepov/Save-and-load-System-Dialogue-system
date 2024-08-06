using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Pawn : MonoBehaviour
{
    public Vector3 Scale = Vector3.one;
    public float Speed = 0.01f;


    public Sprite[] sprites;

    public SpriteRenderer renderer;



    ///Components here 
    private BoxCollider collider;
    private Rigidbody rigidbody;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();

    }


    // Start is called before the first frame update
    void Start()
    {
        // create the collider on runtime to standardize the whole process

         collider = gameObject.AddComponent<BoxCollider>();
         collider.size = Scale;

         rigidbody = gameObject.AddComponent<Rigidbody>();
         rigidbody.freezeRotation = true;


    }

    // Update is called once per frame
    void Update()
    {
        //if player let this fella be controlled




    }

    public virtual void MovePawn(Vector3 movement)
    {
        transform.position += movement.normalized * Speed;
    }

    public virtual void ChangeSprite(Vector3 target)
    {

        if ((target).normalized != new Vector3(0, 0, 0))
        {
            // split whole process into 3 parts , might simplify later and simply flip sprite (if possible)
            if ((target).normalized.x > 0.2f)
            {

                if ((target).normalized.y < -0.4f)
                {
                    renderer.sprite = sprites[3];
                }
                else if ((target).normalized.y > 0.4f)
                {
                    renderer.sprite = sprites[1];
                }
                else
                {
                    renderer.sprite = sprites[2];
                }


            }
            else if ((target).normalized.x < -0.2f)
            {
                if ((target).normalized.y < -0.4f)
                {
                    renderer.sprite = sprites[5];
                }
                else if ((target).normalized.y > 0.4f)
                {
                    renderer.sprite = sprites[7];
                }
                else
                {
                    renderer.sprite = sprites[6];
                }


            }
            else
            {
                if ((target).normalized.y < -0.4f)
                {
                    renderer.sprite = sprites[4];
                }
                else if ((target).normalized.y > 0.4f)
                {
                    renderer.sprite = sprites[0];
                }

            }

        }
      
    }
}
