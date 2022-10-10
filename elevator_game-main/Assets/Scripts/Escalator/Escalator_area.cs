using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escalator_area : MonoBehaviour
{
    public Escalator_area area;
    [HideInInspector]
    public bool isMove = false;
    public bool move=false;
    private Collider2D check;
    public bool Top; //True for Top, False for Bottom
    public Transform endSpot; //Location of opposite escalator area
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Top)
        {
            if (Input.GetKeyDown(KeyCode.S) && check != null)
            {
                move = true;

            }
        }
        else if (!Top)
        {
            if (Input.GetKeyDown(KeyCode.W) && check != null)
            {
                move = true;

            }
        }

        
       
    }

    void OnTriggerStay2D(Collider2D other)
    {
        check = other;
        if (other.gameObject.GetComponent<attack>() &&  move)
        {
            move = false;
            area.move = false;
            isMove = true;
            area.isMove=false;
        }
        move = false;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        check = null;
    }


}
