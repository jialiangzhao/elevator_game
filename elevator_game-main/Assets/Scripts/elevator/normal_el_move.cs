using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normal_el_move : MonoBehaviour
{
    [Tooltip("Let line of sight be visible, for Dev Purposes")]
    private AudioSource audioEle;
    public bool ToggleLineVisibility = false;
    [Tooltip("If the player is inside, let player control elevators movement")]
    public bool inPlayerControl; //We need to allow for the player to control the elevators movement while riding it
    public LayerMask WallMask;
    public float speed;
    [Tooltip("How long does the elevator wait when it's stopped?")]
    public float ElevatorWaitTime;
    private bool go_up;
    bool Waiting; //Elevator should pause when we hit a new floor so people can get on and off
    bool shouldPause;
    public float StopLocationBottom;
    public float StopLocationTop;
    public float RoofLoc;
    public float FloorLoc;

    void Start()
    {
        shouldPause = true;
        audioEle = this.GetComponent<AudioSource>();
        //StartCoroutine(Movement());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position - transform.up * StopLocationTop, transform.right * 0.6f, Color.green);
        Debug.DrawRay(transform.position - transform.up * StopLocationBottom, transform.right * 0.6f, Color.green);
        if (!inPlayerControl)
        {
            Movement();
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (!RoofCheck())
                {
                    transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
                }
                
            }
            else if (Input.GetKey(KeyCode.S))
            {
                if (!FloorCheck())
                {
                    transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
                }
                
            }
        }
    }

    void Movement()
    {



        if (go_up)
        {
            if (ToggleLineVisibility)
            {
                Debug.DrawRay(transform.position - transform.up * StopLocationBottom, transform.right * 0.6f, Color.green);
            }
                
            //Draw a different ray depending on if we're going up or down, because that matters
            if (Physics2D.Raycast(transform.position - transform.up * StopLocationBottom, transform.right, 0.6f, WallMask) && shouldPause) //Check if we've hit a new floor, if so, stop
            {
                //print("Hit a New Floor!");
                StartCoroutine(HitFloorPause());
                StartCoroutine(Pause());

            }
            
            if (!RoofCheck() && !Waiting)
            {


                transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
            }
            else if (RoofCheck() && !Waiting)
            {
                StartCoroutine(Pause());


                go_up = false;
            }
        }
        else
        {
            if (ToggleLineVisibility)
            {
                Debug.DrawRay(transform.position - transform.up * StopLocationTop, transform.right * 0.6f, Color.green); 
            }
            //Draw a different ray depending on if we're going up or down, because that matters
            if (Physics2D.Raycast(transform.position - transform.up * StopLocationTop, transform.right, 0.6f, WallMask) && shouldPause) //Check if we've hit a new floor, if so, stop
            {
                //print("Hit a New Floor!");
                StartCoroutine(HitFloorPause());
                StartCoroutine(Pause());

            }
            
            if (!FloorCheck() && !Waiting)
            {

                transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
            }
            else if (FloorCheck() && !Waiting)
            {

                StartCoroutine(Pause());
                go_up = true;
            }
        }


    }

    bool FloorCheck()
    {
        if (ToggleLineVisibility)
        {
            Debug.DrawRay(transform.position - transform.up * FloorLoc, -transform.up * 0.2f, Color.green);
        }
        return Physics2D.Raycast(transform.position - transform.up * FloorLoc, -transform.up, 0.2f, WallMask);

    }

    bool RoofCheck()
    {
        if (ToggleLineVisibility)
        {
            Debug.DrawRay(transform.position + transform.up * RoofLoc, transform.up * 0.2f, Color.green);
        }
        return Physics2D.Raycast(transform.position + transform.up * RoofLoc, transform.up, 0.2f, WallMask);

    }

    IEnumerator Pause()
    {
        //print("Waiting");
        Waiting = true;
        yield return new WaitForSeconds(ElevatorWaitTime);
        Waiting = false;
    }

    IEnumerator HitFloorPause() //Stop Drawing a ray for a few moments after hitting a new floor so we don't get stuck waiting on every new floor
    {
        shouldPause = false;
        yield return new WaitForSeconds(ElevatorWaitTime + 0.1f);
        shouldPause = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            collision.gameObject.GetComponent<playerMove>().InElevator = true;
            if (!audioEle.isPlaying)
            {
                audioEle.Play();

            } //walk
            //print("Player in Elevator!");
            inPlayerControl = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            collision.gameObject.GetComponent<playerMove>().InElevator = false;
            audioEle.Pause();
            //print("Player Left Elevator!");
            inPlayerControl = false;
        }
    }
}
