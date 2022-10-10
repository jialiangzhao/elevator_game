using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicMove_wj : MonoBehaviour

{
    //     public float speed=3f;
    //     // Update is called once per frame
    //     void Update()
    //     {
    //         transform.Translate(Vector3.up*speed*Time.deltaTime);
    //     }
    // }


    //adjust this to change speed
    [SerializeField]
    float speed = 5f;
    //adjust this to change how high it goes
    [SerializeField]
    float width = 0.5f;

    Vector3 pos;

    private void Start()
    {
        pos = transform.position;
    }
    void Update()
    {

        //calculate what the new Y position will be
        float newX = Mathf.Sin(Time.time * speed) * width + pos.x;
        //set the object's Y to the new calculated Y
        transform.position = new Vector3(newX,transform.position.y, transform.position.z);
    }
}
