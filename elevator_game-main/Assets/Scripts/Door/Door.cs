using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator doorOpen;
    private bool inDoor=false;
    public bool DisguiseDoor;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 && collision.gameObject.GetComponent<playerMove>())
        {
            playerMove player = collision.gameObject.GetComponent<playerMove>();
            player.InRangeofDoor = true;
            if (player.InDoor)
            {
                doorOpen.Play("open", 0, 0f);
                inDoor = true;
                if (DisguiseDoor)
                {
                    collision.gameObject.GetComponent<playerMove>().Disguised = true;
                    print("Giving Player Disguise!");
                }



                // StartCoroutine(keep_close());

            }
            

        }
        if (collision.gameObject.GetComponent<playerMove>())
        {
            if (!collision.gameObject.GetComponent<playerMove>().InDoor && inDoor)
            {
                doorOpen.Play("open", 0, 0f);
                
                inDoor = false;


            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 && collision.gameObject.GetComponent<playerMove>())
        {
            collision.gameObject.GetComponent<playerMove>().InRangeofDoor = false;
            
        }
    }
  

    //IEnumerator keep_close()
    //{
 

    //    yield return new WaitForSeconds(0.5f);
    //    doorOpen.SetBool("open", false);
    //}
}
