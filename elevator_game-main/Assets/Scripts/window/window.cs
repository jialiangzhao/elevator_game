using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class window : MonoBehaviour
{
    public bool right;
    // Start is called before the first frame update
    void Start()
    {
        if (this.transform.localRotation.eulerAngles.y == 180) { 
            right = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
