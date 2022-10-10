using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;



    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 3f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) //Collide with player
    {
        if (collision.gameObject.layer == 8)
        {
            //Do some Damage here
            playerMove player = collision.gameObject.GetComponent<playerMove>();
            if (player!=null) {
                player.player_dead();
            }
            Destroy(this.gameObject);
        }
    }
}
