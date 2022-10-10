using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blood : MonoBehaviour
{

    public float destroy_time;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator destroy()
    {
        yield return new WaitForSeconds(destroy_time);
        Destroy(this.gameObject);

    }
}
