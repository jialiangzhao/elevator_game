using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escalator_move : MonoBehaviour
{
    public Escalator_area top;
    public Escalator_area bottom;
    public GameObject player;
    public float speed;
 
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        /*
                 if (bottom.isMove==true) {

            player.GetComponent<playerMove>().enabled = false;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            player.transform.position = Vector2.MoveTowards(player.transform.position, top.transform.position, speed * Time.deltaTime);
        }
        if (player.transform.position == top.transform.position)
        {

            player.GetComponent<playerMove>().enabled = true;
            player.GetComponent<Rigidbody2D>().isKinematic = false;
            bottom.isMove = false;
        }

        if (top.isMove == true)
        {
           
            player.GetComponent<playerMove>().enabled = false;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            player.transform.position = Vector2.MoveTowards(player.transform.position, bottom.transform.position, speed * Time.deltaTime);
           
        }
        if (player.transform.position == bottom.transform.position)
        {
            player.GetComponent<playerMove>().enabled = true;
            player.GetComponent<Rigidbody2D>().isKinematic = false;
            top.isMove = false;
        }
         
         
         */




    }

}
