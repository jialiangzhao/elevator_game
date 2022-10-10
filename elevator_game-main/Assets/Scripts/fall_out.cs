using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fall_out : MonoBehaviour
{
    public AudioSource audioFall;
    public LayerMask ground;
    public LayerMask elevator;
    private Collider2D foot;
    public float time;
    public float temp_time;
    // Start is called before the first frame update
    void Start()
    {
        temp_time=time;
        if (this.gameObject.GetComponent<BoxCollider2D>())
        {
            foot = this.GetComponent<BoxCollider2D>();
        }
        else {
            foot = this.GetComponent<CapsuleCollider2D>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (!(foot.IsTouchingLayers(ground) || foot.IsTouchingLayers(elevator)))
        {
            temp_time -= Time.deltaTime;
        }
        else {
            temp_time = time;
        }
        if (this.gameObject.GetComponent<BoxCollider2D>()) {
            if (this.transform.parent.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Static)
            {
                temp_time = time;
            }
        }
         
        if (temp_time <= 0) 
        {
            if (!audioFall.isPlaying)
            {
                audioFall.Play();
            }
            if (this.transform.parent.GetComponent<attack>() )
            {
                
                    print("player_fall_out");
                    this.transform.parent.GetComponent<playerMove>().player_dead();
                
            }
            else
            {
                
                    this.transform.gameObject.GetComponent<Enemy>().dead();
                
            }
        }
        
    }

    public void temp_reset() {
        temp_time = time;
    }


}
