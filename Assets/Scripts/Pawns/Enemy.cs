using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Enemy : Pawn
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private BoxCollider CombatTrigger;

    private Vector3 lastPosition;


    public bool Dead;

    [SerializeField] private CombatEncounter Fight;


    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.AddComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        agent.speed = Speed;
        agent.SetDestination(new Vector3(1000, 100, 1000));

        CombatTrigger = gameObject.AddComponent<BoxCollider>();
        CombatTrigger.size = Scale * 1.2f;
        CombatTrigger.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Dead)
        {
            agent.SetDestination(new Vector3(0, 0, 0));
        }

        
        
        // Determening sprite 

        ///How its done: 
        ///1) get direction to current destination
        ///2) See the x and y components of that 
        ///3)depending on value and sign we can determinet sprite


        ///   (-x,y)(~0,y)(x,y)
        ///   (-x,~0)(0,0)(x,~0)
        ///   (-x,-y)(~0,-y)(x,-y)
        ///   sprite sheet looks something like this for player we use same logic but use the input instead
        ///   later on will actually have array of animations rather than sprites when making walking animations


        if (Time.frameCount % 10 == 0 && !Dead)
        {
            lastPosition = transform.position;
        }

        //print((transform.position - lastPosition).normalized);
        ChangeSprite(Vector3.zero);
    }


    public override void ChangeSprite(Vector3 target)
    {
        base.ChangeSprite(target);

        if ((transform.position - lastPosition).normalized != new Vector3(0, 0, 0))
        {
            // split whole process into 3 parts , might simplify later and simply flip sprite (if possible)
            if ((transform.position - lastPosition).normalized.x > 0.2f)
            {

                if ((transform.position - lastPosition).normalized.y < -0.4f)
                {
                    renderer.sprite = sprites[3];
                }
                else if ((transform.position - lastPosition).normalized.y > 0.4f)
                {
                    renderer.sprite = sprites[1];
                }
                else
                {
                    renderer.sprite = sprites[2];
                }


            }
            else if ((transform.position - lastPosition).normalized.x < -0.2f)
            {
                if ((transform.position - lastPosition).normalized.y < -0.4f)
                {
                    renderer.sprite = sprites[5];
                }
                else if ((transform.position - lastPosition).normalized.y > 0.4f)
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
                if ((transform.position - lastPosition).normalized.y < -0.4f)
                {
                    renderer.sprite = sprites[4];
                }
                else if ((transform.position - lastPosition).normalized.y > 0.4f)
                {
                    renderer.sprite = sprites[0];
                }

            }

        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<Player>() && !Dead)
        {
            //start fight, will have to carry over the data of the enemy this enemy and save positions to keep positions
            //after loading back into overworld after fight 


            FindObjectOfType<CombatManager>().CurrentEncounter = Fight;
            SceneManager.LoadScene(1);
        }
    }


    public void Die()
    {
        Dead = true;
        agent.isStopped = true;
    }
}
