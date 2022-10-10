using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class el_move : MonoBehaviour
{

    public float top;
    public float bottom;
    public float speed;
    public float min_speed;
    private bool go_up;
    private float add_speed;
    private float temp_speed;
    private bool test;

    void Start()
    {
        add_speed = (top - bottom) / 5000 / speed;
        temp_speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        test = go_up;
        
        if (temp_speed <= min_speed) {
            temp_speed = min_speed;
        }
        if (transform.position.y <= top && go_up==true)
        {
            
            temp_speed = temp_speed - (add_speed/2);
            transform.Translate(Vector3.up * temp_speed * Time.deltaTime, Space.World);
        }
        else
        {
            
            
            go_up = false;
        }
        
        if (transform.position.y >= bottom && go_up == false)
        {

           
            temp_speed = temp_speed + add_speed*40;
        
            transform.Translate(Vector3.down * temp_speed * Time.deltaTime, Space.World);
        }
        else
        {
          
                
           
            go_up = true;
        }

        if (test != go_up) {
            temp_speed = speed;

        }

    }
}
