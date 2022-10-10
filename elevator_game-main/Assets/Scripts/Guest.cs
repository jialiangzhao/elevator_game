using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest : NPC
{
    public Animator g_animator;
    public static event AlertOthers AlertAction;
    public bool isTarget;
    public playerMove player;

    void Start()
    {
        StartCoroutine(Patrol());
    }

    private void OnEnable()
    {
        playerMove.Stealth += UnAlert;
        player = GameObject.FindObjectOfType<playerMove>();
        Enemy.AlertAction += Alert;
        AlertAction += Alert;
        playerMove.BroadcastLocation += GetPlayerLoc;
    }

    private void OnDisable()
    {
        playerMove.Stealth -= UnAlert;
        Enemy.AlertAction -= Alert;
        AlertAction -= Alert;
        playerMove.BroadcastLocation -= GetPlayerLoc;
    }

    private void Update()
    {
        FloorCheck();
        if (IsPlayerInRange() && !alerted)
        {


             if (player.Bloodstained) //Do NOT alert if player is disguised. 
            {
                AlertAction();
                
            }
            
        }
    }

    public void GetPlayerLoc(Vector3 Loc)
    {
        PlayerLocation = Loc;
        print("Getting Player Location!");
    }

    public void UnAlert()
    {
        alerted = false;
    }

    private bool isdead=false;
    public void dead()
    {
        if (!isdead)
        {

            player.StartCoroutine(player.JustKilled());
            isdead = true;        
            Destroy(this.gameObject);
        }

    }
    private void FixedUpdate()
    {
        //print(this.rb.velocity.magnitude);
        if (!g_animator.GetBool("dead"))
        {
            g_animator.SetFloat("speed", this.rb.velocity.magnitude);
        }
        //Set Idle/Walk
        //print(e_animator.GetFloat("Speed"));
    }

}
