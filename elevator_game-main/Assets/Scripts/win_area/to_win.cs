using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class to_win : MonoBehaviour
{
    public string win_name;
    public float speed;
    public GameObject player;
    public GameObject ufo_object;
    private bool start = false;
    private float x;
    private bool ufo = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ufo) {
            var direction = new Vector3(
                player.transform.position.x,
                player.transform.position.y+0.5f,0) - ufo_object.transform.position;
            ufo_object.transform.Translate(direction.normalized * Time.deltaTime * 5f, Space.World);

        }
        if (start) {
            player.transform.GetChild(0).DetachChildren();
           
            
            player.transform.localPosition = new Vector2(
            player.transform.position.x, 
            player.transform.position.y + speed*Time.deltaTime);
            player.transform.localScale *= 0.999f;
            x += Time.deltaTime * 500;
            player.transform.rotation = Quaternion.Euler(0, 0, x);
           
        }
        
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<attack>())
        {
            ufo = true;
            player.GetComponent<playerMove>().enabled = false;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            StartCoroutine(win_ufo(other));
          
           
            

        }
    }

    IEnumerator win_wait(Collider2D other)
    {

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(win_name);


    }

    IEnumerator win_ufo(Collider2D other)
    {

        yield return new WaitForSeconds(2);
        ufo_object.transform.GetChild(0).gameObject.SetActive(true);
        start = true;
        ufo = false;
        StartCoroutine(win_wait(other));

    }





}
