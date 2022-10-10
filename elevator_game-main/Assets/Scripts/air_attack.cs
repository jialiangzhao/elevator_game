using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class air_attack : MonoBehaviour
{
    public float air_power;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        
     
        if (other.gameObject.GetComponent<repulse>())
        {
            other.gameObject.GetComponent<repulse>().isRepulse = true;
            other.GetComponent<Rigidbody2D>() .AddForce(new Vector2(air_power* player.transform.localScale.x, 200));
           
        }
    }
}
