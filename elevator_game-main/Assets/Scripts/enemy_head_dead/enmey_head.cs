using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enmey_head : MonoBehaviour
{
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
       
        if (other.gameObject.GetComponent<fall_out>())
        {
            other.gameObject.GetComponent<fall_out>().temp_reset();
            other.gameObject.transform.parent.gameObject.GetComponent<playerMove>().player_jump();
            StartCoroutine(enemyDead());

        }
    }

    IEnumerator enemyDead()
    {

        yield return new WaitForSeconds(1);

        Destroy(this.gameObject.transform.parent.gameObject);


    }
}
