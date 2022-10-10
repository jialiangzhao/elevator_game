using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wenjia_movementtest : MonoBehaviour
{
    // Start is called before the first frame update
   // void Start()
 //   {
        
   // }

    public Animator mc_animator;
    public Rigidbody2D rb;
    public AudioSource audioSource;
    //public AudioClip feet;

    public float jumpForce = 20f;
    private bool onGround = false;
    

    // Update is called once per frame
    void Update()
    {

    


        mc_animator.SetFloat("Horizontal",Input.GetAxis("Horizontal")); //walk

        if (Input.GetAxis("Horizontal") >= 0.1 || Input.GetAxis("Horizontal") <= -0.1)
        {

            mc_animator.SetFloat("Horizontal", 1);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();

            } //walk
        }
        else {
            mc_animator.SetFloat("Horizontal", 0);
            audioSource.Pause(); //walk
        }

        float facedirection = Input.GetAxisRaw("Horizontal");

        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);

        }
        mc_animator.SetBool("Jump", Input.GetKey(KeyCode.Space)); //


        mc_animator.SetBool("Crouch", Input.GetKey(KeyCode.S)); //crouch
        mc_animator.SetBool("FistAttack", Input.GetKey(KeyCode.F)); //attack

        Vector3 horizontal= new Vector3(Input.GetAxis("Horizontal"), 0.0f,0.0f);
        transform.position=transform.position+horizontal*Time.deltaTime;

       // Movement();

        if (Input.GetKeyDown(KeyCode.Space) && onGround == true) //&& jump_good == true
        {
        
            Jump();
        }

    //     if (Input.GetAxis("Horizontal") < 0)
    //   {
    //       transform.Translate(Vector2.right  * Time.deltaTime);
    //       transform.rotation = Quaternion.Euler(0, 180f, 0f);
    //       ruyinAnimation.CrossFade("walk_left");
    //   }

    }



    // void Movement()
    // {

// }//



void Jump(){

    Vector2 vertical= new Vector2(rb.velocity.x,jumpForce);
    rb.velocity=vertical;

            onGround = false;
}

 void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Ground")
        {
            onGround = true;
        }
        
    }
 
    //Check if the object has left the ground
  //  void OnCollisionExit(Collision collisionInfo)
   // {
  //      if(collisionInfo.gameObject.CompareTag("Ground"))
    //    {
      //      onGround = false;
    //    }
   // }
}

