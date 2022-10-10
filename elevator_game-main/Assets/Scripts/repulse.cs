using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repulse : MonoBehaviour
{
    public bool isRepulse;
    public LayerMask window;
    public GameObject right_window;
    public GameObject left_window;
    // Start is called before the first frame update
    void Start()
    {
        isRepulse = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRepulse) {
            StartCoroutine(repulse_fun());
        }
      

       

    }

    IEnumerator repulse_fun()
    {
        yield return new WaitForSeconds(1);
        isRepulse=false;
    }

    void OnTriggerStay2D(Collider2D other)
    {



        if (other.gameObject.GetComponent<window>()&& isRepulse)
        {
            if (other.gameObject.GetComponent<window>().right)
            {
                Instantiate(right_window,
                  other.gameObject.transform.position,
                  other.gameObject.transform.rotation);
            }
            else {
                Instantiate(left_window,
                  other.gameObject.transform.position,
                  other.gameObject.transform.rotation);
            }
            Destroy(other.gameObject);

        }
    }

}
