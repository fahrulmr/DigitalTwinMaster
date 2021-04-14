using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationcontrol : MonoBehaviour
{
     public Animator CNC;
     public bool cncbuka;

    void Start()
    {
        CNC = GetComponent<Animator>();
    }
    void Update()
    {
        CNC.SetBool("buka",cncbuka);

        if (Input.GetKeyDown(KeyCode.A))
        {
            CNC.gameObject.GetComponent<Animator>().enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            cncbuka = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            cncbuka = false;
        }
        
    }
}
