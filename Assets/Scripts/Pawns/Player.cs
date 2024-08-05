using UnityEngine;

public class Player : Pawn
{
    private Vector3 movement;
    public Interactable Interactable;

    [SerializeField] Transform PlayerModel;


    private LineRenderer Line;
    [HideInInspector] public float savedSpeed;
    private bool isAiming;
    private Transform shootingPosition;
    [SerializeField] float TargetZ;

    public static Player Instance;


    private void Awake()
    {
        Line = GetComponent<LineRenderer>();
        savedSpeed = Speed;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;

        shootingPosition = transform.GetChild(0);
    }

    void FixedUpdate()
    {
        //if player let this fella be controlled

        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //  mouseWorldPos.z = -2f;


        MovePawn(movement);

        if (Input.GetMouseButton(1))
        {
            Aim(mouseWorldPos);
            isAiming = true;
        }
        else if(Input.GetMouseButtonUp(1))
        {
            StopAiming();
            isAiming = false;
        }


        if (isAiming)
        {
            ChangeSprite(-(transform.position - mouseWorldPos));
        }
        else
        {
            ChangeSprite(movement);
        }


        if ((movement.x != 0 || movement.z != 0) && !isAiming && Speed!=0)
        {
            PlayerModel.rotation = Quaternion.Slerp(PlayerModel.rotation, Quaternion.LookRotation(movement), 0.15f);
        }
    }

    private void Update()
    {
        if (Interactable && Input.GetKeyDown(KeyCode.Space))
        {
            Interactable.Interact(this);
        }
    }

    private void Aim(Vector3 target)
    {
        //  PlayerModel.rotation = Quaternion.LookRotation(target);


        shootingPosition.position = PlayerModel.position - (PlayerModel.position - target).normalized;

        /*
        shootingPosition.position = transform.position - (transform.position - target).normalized;


        RaycastHit2D hit = Physics2D.Raycast(shootingPosition.position, target - transform.position, Vector2.Distance(shootingPosition.position, target));


        if (hit)
        {
            Debug.Log(hit.transform.gameObject.name);
        }
        */


        /*Line.SetPosition(0, shootingPosition.position);
        if (hit)
        {
            Line.SetPosition(1, hit.point);
        }
        else
        {
            Line.SetPosition(1, target);
        }*/

        Vector3 hitPosition;


        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            hitPosition = hit.point;
            ///   shootingPosition.position = hitPosition + new Vector3(0,1,0);
        }


        RaycastHit hit2;

        if (Physics.Raycast(shootingPosition.position, (hit.point - shootingPosition.position), out hit2))
        {
            Line.SetPosition(0, shootingPosition.position);
            Line.SetPosition(1, hit2.point);
        }

        //rotate aim point to face the new position

        //make another raycast checking between aim point and  hit point

        //make a line between aim position and the place where ray hit 

        // make a physics.sphere check where hitPosition is

        AllowMovement(false);
    }


    public void AllowMovement(bool condition)
    {
        if (condition)
        {
            Speed = savedSpeed;
        }
        else
        {
            Speed = 0;
        }
    }

    private void StopAiming()
    {
        Line.SetPosition(0, transform.position);
        Line.SetPosition(1, transform.position);
        AllowMovement(true);
    }
}
