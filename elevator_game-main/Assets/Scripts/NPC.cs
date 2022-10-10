using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public delegate void AlertOthers();

    [Tooltip("So the Enemy distinguishes between player and other entities")]
    public LayerMask PlayerMask;
    [Tooltip("So the Enemy detects a wall in front of them to turn around")]
    public LayerMask WallMask;
    [Tooltip("How far does the enemy perceive players?")]
    public float LineOfSightDistance;
    [Tooltip("Let line of sight be visible, for Dev Purposes")]
    public bool ToggleLineVisibility = false;
    [Tooltip("How Close do we have to be to a wall/floor to turn around?")]
    public float DistanceCheck;
    public float PatrolSpeed;
    public float ChaseSpeed;
    public Vector3 PlayerLocation;
    public bool alerted = false;
    public bool InEscalator;
    public bool InEscalatorArea;
    public Vector3 EscalatorDestination;

    public Rigidbody2D rb;

    public bool Waiting;


    public bool IsPlayerInRange()
    {
        //RaycastHit2D Hit = Physics2D.Raycast(transform.position, transform.forward * LineOfSightDistance);
        if (ToggleLineVisibility)
        {
            Debug.DrawRay(transform.position + transform.up * 0.1f, transform.right * LineOfSightDistance, Color.green); // Debug purposes
        }

        return (Physics2D.Raycast(transform.position, transform.right, LineOfSightDistance, PlayerMask));
    }

    public IEnumerator Patrol()
    {
        while (!alerted)
        {
            if (!WallCheck() && FloorCheck() && !InEscalator) //Move if we DON'T hit a wall and if we DO hit a floor
            {
                rb.velocity = transform.right * PatrolSpeed;
            }
            else
            {
                if (!InEscalator)
                {
                    rb.velocity = new Vector3(0, 0, 0);
                }
                //print("Waiting");
                yield return new WaitForSeconds(2);
                TurnAround();
            }
            yield return new WaitForSeconds(0.1f);
        }

    }


    public void TurnAround() //If you hit a wall or pit, turn around
    {
        //print("Turning Around");
        if (transform.right == Vector3.right) //Which way are we facing? Which way should we turn towards?
        {
            //print("Right");
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            //print("Left");
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }

    }

    public bool WallCheck() //Check if we're in front of a wall. If true, stop walking
    {
        if (ToggleLineVisibility)
        {
            Debug.DrawRay(transform.position, transform.right * DistanceCheck, Color.red);
        }

        return (Physics2D.Raycast(transform.position, transform.right, DistanceCheck, WallMask));
    }
    //Draw a line Down
    //Returns false if there's a pit in front
    public bool FloorCheck() //Check if there's floor in front of us, i.e., if we hit a pit without an elevator to stop us
    {
        if (ToggleLineVisibility)
        {
            //Debug.DrawRay(transform.position + transform.right * DistanceCheck, -transform.up, Color.red);
            Debug.DrawRay(transform.position + transform.right * DistanceCheck / 4, -transform.up, Color.red);
        }

        return (Physics2D.Raycast(transform.position + transform.right * DistanceCheck / 4, -transform.up, 1, WallMask));
    }

    public void TakeEscalator()
    {
        //Take a random value to TakeEscalator if they're patrolling
        InEscalator = true;
        rb.bodyType = RigidbodyType2D.Static;
    }



    public void RidingEscalator()
    {
        transform.position = Vector2.MoveTowards(transform.position, EscalatorDestination, PatrolSpeed * Time.deltaTime);
        if (transform.position == EscalatorDestination)
        {
            InEscalator = false;
            rb.isKinematic = false;
        }
    }

    public virtual void Alert()
    {
        alerted = true;
        //print(Loc);
    }
}
