using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class window_fly : MonoBehaviour
{
    private float randomX;
    private float randomY;
    public float right;
    // Start is called before the first frame update
    void Start()
    {
        randomX = Random.Range(0f, right*400f);
        randomY = Random.Range(-300f, 300f);
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(randomX, randomY));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
