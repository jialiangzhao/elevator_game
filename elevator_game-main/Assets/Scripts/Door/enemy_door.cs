using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class enemy_door : MonoBehaviour
{
    private float create_time;
    public Animator door_animator;
    public GameObject enemy;
    private float time;
    private float a;
    public UnityEngine.Camera cam;
    public bool isCheck;
    public GameObject all;
    // Start is called before the first frame update
    void Start()
    {
        door_animator = this.GetComponent<Animator>();
        create_time = Random.Range(2.0f, 10.0f);
        time =create_time;
         a = Random.Range(-10.0f, 28.0f);
        cam = UnityEngine.Camera.main;
        isCheck= true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewPos = cam.WorldToViewportPoint(this.transform.position);
        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {
            // Your object is in the range of the camera, you can apply your behaviour
            isCheck = false;
        }
        else
            isCheck = true;

        if (!isCheck)
            create();



    }


    void create() {
        time -= Time.deltaTime;


        if (time <= 0 )
        {
            if (a < 0 && all.transform.childCount<=6)
            {
                door_animator.Play("door_open", 0, 0f);
                 door_animator.SetBool("open",true);
                GameObject guy=Instantiate(enemy, this.gameObject.transform.position, Quaternion.identity);
                guy.transform.parent = all.transform;
            }


            time = create_time;
            a = Random.Range(-10.0f, 28.0f);

        }
    }
  
}
