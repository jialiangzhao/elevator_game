using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class target_head : MonoBehaviour
{
 
    public GameObject targeta;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targeta.transform.childCount == 4) {
            this.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(87, 87, 87, 100); 
        }
        if (targeta.transform.childCount == 3)
        {
            this.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(87, 87, 87, 100);
            this.transform.GetChild(1).gameObject.GetComponent<Image>().color = new Color32(87, 87, 87, 100);
        }
        if (targeta.transform.childCount == 2)
        {
            this.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(87, 87, 87, 100);
            this.transform.GetChild(1).gameObject.GetComponent<Image>().color = new Color32(87, 87, 87, 100);
            this.transform.GetChild(2).gameObject.GetComponent<Image>().color = new Color32(87, 87, 87, 100);
        }
        if (targeta.transform.childCount == 1)
        {
            this.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(87, 87, 87, 100);
            this.transform.GetChild(1).gameObject.GetComponent<Image>().color = new Color32(87, 87, 87, 100);
            this.transform.GetChild(2).gameObject.GetComponent<Image>().color = new Color32(87, 87, 87, 100);
            this.transform.GetChild(3).gameObject.GetComponent<Image>().color = new Color32(87, 87, 87, 100);
        }
        if (targeta.transform.childCount == 0)
        {
            this.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(87, 87, 87, 100);
            this.transform.GetChild(1).gameObject.GetComponent<Image>().color = new Color32(87, 87, 87, 100);
            this.transform.GetChild(2).gameObject.GetComponent<Image>().color = new Color32(87, 87, 87, 100);
            this.transform.GetChild(3).gameObject.GetComponent<Image>().color = new Color32(87, 87, 87, 100);
            this.transform.GetChild(4).gameObject.GetComponent<Image>().color = new Color32(87, 87, 87, 100);
        }

    }
}

