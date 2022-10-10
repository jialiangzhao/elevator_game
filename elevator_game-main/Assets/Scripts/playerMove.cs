using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class playerMove : MonoBehaviour
{
    public Transform flag;
    public GameObject all_E;
    public delegate void Broadcast(Vector3 Loc);
    public static event Broadcast BroadcastLocation;

    public delegate void EnterStealth(); //Broadcast when the player enters door or gets disgyuise, Un-Alert Enemies
    public static event EnterStealth Stealth;


    //public Animator mc_animator;

    public Animator mc_animator;
    public AudioSource audioWalk;
    public AudioSource audioJump;
    public AudioSource alertSound;
    public Rigidbody2D rb;
    public float speed;
    public float jumpforce;
    public int DisguiseLength; //How long does our disguise last?
    int BaseDisguiseLength;
    public LayerMask ground;
    public LayerMask headCheck;
    private bool jump_good = false;
    public Collider2D coll;
    public Collider2D head;
    public bool squat;
    public bool spotted;
    private float player_high;
    private float player_weigth;
    private float headCheck_y;
    private float headCheck_x;
    [HideInInspector]
    public float facedirection;
    public float horizontalmove ;
    public string lose_name;
    public bool InDoor;
    public bool InRangeofDoor;
    public bool Disguised;
    public bool InElevator;
    private bool InEscalator;
    private bool InEscalatorArea;
    public bool Bloodstained; //Player has just killed, if a guest spots them while this is true, activate alert.
    public Vector3 EscalatorDestination;
    public float wait_time;
    private float temp_time = 0;
    // Update is called once per frame

    void Start() {
        BaseDisguiseLength = DisguiseLength;
        headCheck_y =head.transform.localPosition.y;
        headCheck_x = head.transform.localPosition.x;
        player_high = this.GetComponent<CapsuleCollider2D>().size.y;
        player_weigth = this.GetComponent<CapsuleCollider2D>().size.x;
        facedirection = Input.GetAxisRaw("Horizontal");
        horizontalmove = Input.GetAxis("Horizontal");
    }
    private void OnEnable()
    {
        Guest.AlertAction += Spotted;
        Enemy.AlertAction += Spotted;
    }

    public void OnDisable()
    {
        Guest.AlertAction -= Spotted;
        Enemy.AlertAction -= Spotted;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //mc_animator.SetFloat("Horizontal",Input.GetAxis("Horizontal"));
        if (!InDoor && !InEscalator)
        {
            Movement();
        }
        
      
        if (Input.GetButtonDown("EnterDoor") && InRangeofDoor)
        {
            if (!InDoor && temp_time <= 0)
            {
                StartCoroutine(EnterDoor()); 
            }
            else if (InDoor && temp_time <= 0)
            {
                StartCoroutine(ExitDoor()); 
            }

        }
        temp_time -= Time.deltaTime;
        if (InEscalatorArea)
        {
            if (EscalatorDestination.y - transform.position.y > 0) //If the endpoint is Higher, We want to hit W to Ascend
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    InEscalator = true;
                    rb.bodyType = RigidbodyType2D.Static;
                }
            }
            else //Else if the endpoint is lower, we hit S to descend
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    InEscalator = true;
                    rb.bodyType = RigidbodyType2D.Static;
                }
            }
        }

        if (InEscalator)
        {
            transform.position = Vector2.MoveTowards(transform.position, EscalatorDestination, speed * Time.deltaTime);
            if (transform.position == EscalatorDestination)
            {
                InEscalator = false;
                rb.isKinematic = false;
            }
        }




    }
    void Movement()
    {

        horizontalmove = Input.GetAxis("Horizontal");
        facedirection = Input.GetAxisRaw("Horizontal");

        //character move
        if (horizontalmove != 0)
        {
            if (!InDoor)
            {
                if (squat)
                {
                    
                    rb.velocity = new Vector2(horizontalmove * speed * 0.5f, rb.velocity.y);
                }
                else
                {
                    if ((Input.GetAxis("Horizontal") >= 0.1 || Input.GetAxis("Horizontal") <= -0.1)
                        && !mc_animator.GetBool("FistAttack") && !mc_animator.GetBool("Crouch"))
                    {
                       
                            mc_animator.SetFloat("Horizontal", 1);
                        
                        
                       
                        if (!audioWalk.isPlaying)
                        {
                            audioWalk.Play();

                        } //walk
                    }
                    else
                    {
                        mc_animator.SetFloat("Horizontal", 0);
                     
                        audioWalk.Pause(); //walk
                    }
                    rb.velocity = new Vector2(horizontalmove * speed, rb.velocity.y);
                   

                   
                }
            }


        }

        //character turn around
        if (facedirection != 0)
        {
           
            transform.localScale = new Vector3(facedirection, 1, 1);

        }
       
        if (coll.IsTouchingLayers(ground) || coll.IsTouchingLayers(headCheck))
        {
            
            jump_good = true;
        }
        else
        {
            jump_good = false;
        }

        if (head.IsTouchingLayers(headCheck) || head.IsTouchingLayers(ground))
        {
            if (coll.IsTouchingLayers(ground) || coll.IsTouchingLayers(headCheck))
            {
                print("Player dies due to being squashed");
                player_dead();
                
            }
            
        }

        if (!InRangeofDoor)
        {
            mc_animator.SetBool("Jump", Input.GetKey(KeyCode.W) && !InElevator);
        }
        

        mc_animator.SetBool("Crouch", squat);
        if (mc_animator.GetBool("Crouch")) {
            mc_animator.SetFloat("Horizontal", 0);
        }
            
        //crouch


        if (Input.GetButtonDown("Jump") && jump_good == true && !InRangeofDoor && !InElevator)
        {
            
            audioJump.Play();

            
            rb.AddForce(new Vector2(0, jumpforce));
            head.transform.localPosition = new Vector2(headCheck_x, headCheck_y);
            this.GetComponent<CapsuleCollider2D>().offset = new Vector2(0f, 0f);
            this.GetComponent<CapsuleCollider2D>().size = new Vector2(player_weigth, player_high);
           
            squat = false;

        }
        if (Input.GetKeyDown(KeyCode.S)) 
        {
            if (squat)
            {
                
                head.transform.localPosition = new Vector2(headCheck_x, headCheck_y);
                this.GetComponent<CapsuleCollider2D>().offset = new Vector2(0f, 0f);
                this.GetComponent<CapsuleCollider2D>().size = new Vector2(player_weigth, player_high);
                squat = false;

            }
            else 
            {
                if (!InEscalatorArea)
                {
              
                    head.transform.localPosition = new Vector2(headCheck_x, headCheck_y - player_high/2);
                    this.GetComponent<CapsuleCollider2D>().offset = new Vector2(0f, -player_high / 4);
                    this.GetComponent<CapsuleCollider2D>().size = new Vector2(player_weigth, player_high / 2);
                    squat = true;
                }


            }
        }


    }

    public void player_dead() {

       
        Transform[] ts = all_E.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts)
        {
            if (t.gameObject.GetComponent<Enemy>())
            {
                t.gameObject.GetComponent<Enemy>().dead();
            }

        }
       
        StartCoroutine(lose_menu());
        
        
    }

    IEnumerator lose_menu()
    {
        if (!mc_animator.GetBool("dead")) {
            scene_save.score -= 500;
            mc_animator.SetBool("dead", true);
        }
        
        yield return new WaitForSeconds(1);

        if (mc_animator.GetBool("dead"))
        {
            mc_animator.SetBool("dead", false);
        }
       
        this.GetComponent<Transform>().localPosition = 
            new Vector3(flag.localPosition.x, flag.localPosition.y , 0);

        //SceneManager.LoadScene(lose_name);


    }

    public void player_jump() {
        rb.AddForce(new Vector2(0, jumpforce));
        print("Jumping!");
    }

    IEnumerator EnterDoor()
    {
        InDoor = true;
        Stealth();
        print("Entering Door");
        temp_time = wait_time;
        /* rb.constraints = RigidbodyConstraints2D.FreezePosition;*/ //Prevent player from sliding If moving while entering door
        rb.velocity = new Vector2(0, 0);
        // rb.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(1);// wait 1 sec for animation mc go out door
        this.gameObject.layer = 11;
        this.transform.Find("jumpCheck").gameObject.layer = 11;
        this.transform.Find("headCheck").gameObject.layer = 11;
        this.GetComponent<Renderer>().enabled = false;
        
    }

    IEnumerator ExitDoor()
    {
        
        print("Exiting Door");
        rb.velocity = new Vector2(0, 0);
        temp_time = wait_time;
        yield return new WaitForSeconds(1f);// wait 1 sec for animation mc go out door
        if (Disguised)
        {
            StartCoroutine(DisguiseCountdown());
        }
      
        //rb.constraints = RigidbodyConstraints2D.None;
        //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        // rb.isKinematic = false;
        this.gameObject.layer = 8;
        this.transform.Find("jumpCheck").gameObject.layer = 8;
        this.transform.Find("headCheck").gameObject.layer = 8;
        this.GetComponent<Renderer>().enabled = true;
        InDoor = false;
    }

    public IEnumerator JustKilled()
    {
        //print("Committed Murder!");
        Bloodstained = true;
        int bloodied = 3;
        while (bloodied > 0)
        {
            yield return new WaitForSeconds(1f);
            bloodied--;

        }
        Bloodstained = false;
    }

    public IEnumerator DisguiseCountdown()
    {
        print("Getting Disguise!");
        DisguiseLength = BaseDisguiseLength;
        Disguised = true;
        mc_animator.SetBool("Disguised", Disguised);
        while (DisguiseLength > 0)
        {
            yield return new WaitForSeconds(1f);
            DisguiseLength -= 1;
        }
        BreakDisguise();
    }

    public void BreakDisguise()
    {
        
        print("Breaking Disguise!");
        Disguised = false;
        mc_animator.SetBool("Disguised", Disguised);
    }

    public void Spotted()
    {
        spotted = true;
        if (alertSound != null) { 
            if (!alertSound.isPlaying)
            {
                alertSound.Play();

            }
        }
       
        StartCoroutine(LocationDisplayLoop());
    }

    IEnumerator LocationDisplayLoop()
    {
        while (spotted)
        {
            BroadcastLocation(transform.position);
            yield return new WaitForSeconds(3);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            Escalator_area startPoint = collision.gameObject.GetComponent<Escalator_area>();
            InEscalatorArea = true;
            EscalatorDestination = collision.gameObject.GetComponent<Escalator_area>().endSpot.transform.position;


            //print("Hit an escalator");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            InEscalatorArea = false;
        }
    }

}
