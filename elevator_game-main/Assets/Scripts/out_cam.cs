using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class out_cam : MonoBehaviour
{
    public float dead_time;
    private float time;
    public UnityEngine.Camera cam;
    public bool isCheck = false;
    private bool once=true;
    // Start is called before the first frame update
    void Start()
    {
        time = dead_time;

        cam = UnityEngine.Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewPos = cam.WorldToViewportPoint(this.transform.position);
        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {
            isCheck = false;
        } else { isCheck = true; }
            
        time -= Time.deltaTime;
        if (!isCheck) {
            time = dead_time;
        }
        if (time <= 0 && once) {
           
            once = false;
           this.gameObject.GetComponent<Enemy>().dead();
            
        }
    }
}
