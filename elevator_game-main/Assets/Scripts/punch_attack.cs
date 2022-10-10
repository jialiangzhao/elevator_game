using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class punch_attack : MonoBehaviour
{
    public TMP_Text score_text;
    public GameObject blood;
    public Transform flag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score_text.text = scene_save.score.ToString();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<repulse>())
        {
            StartCoroutine(enemyDead(other));

        }
    }

    IEnumerator enemyDead(Collider2D other) {

        if (other.gameObject.GetComponent<Enemy>())
        {
            other.gameObject.GetComponent<Enemy>().e_animator.SetBool("dead", true);
            scene_save.score += 100;
            yield return new WaitForSeconds(0.4f);

            GameObject.FindObjectOfType<playerMove>().StartCoroutine(GameObject.FindObjectOfType<playerMove>().JustKilled());
            other.gameObject.GetComponent<Enemy>().dead();



            Instantiate(blood,
                      other.gameObject.transform.position,
                      other.gameObject.transform.rotation);
        }
        if(other.gameObject.GetComponent<Guest>()) {
            other.gameObject.GetComponent<Guest>().g_animator.SetBool("dead",true);
            if (other.gameObject.GetComponent<Guest>().isTarget)
            {
                flag.localPosition =
                  new Vector3(other.gameObject.transform.position.x, 
                  other.gameObject.transform.position.y, 0);
                scene_save.score += 1000;
            }
            else {
                scene_save.score -= 500;
            }
            
            yield return new WaitForSeconds(0.8f);

            GameObject.FindObjectOfType<playerMove>().StartCoroutine(GameObject.FindObjectOfType<playerMove>().JustKilled());
            other.gameObject.GetComponent<Guest>().dead();
        }
      
    }



}
