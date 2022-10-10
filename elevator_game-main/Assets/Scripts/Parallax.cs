using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform Cam;
    public float moveRate;
    public float xFix;
    public float yFix;
    private float startPointX;
    private float startPointY;
    // Start is called before the first frame update
    void Start()
    {
        startPointX = transform.position.x-xFix * moveRate;
        startPointY = transform.position.y+ yFix * moveRate;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = new Vector2(startPointX + Cam.position.x * moveRate, startPointY- Cam.position.y * moveRate);
    }
}
