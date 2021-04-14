using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotasi : MonoBehaviour
{
    public GameObject robot;
    public GameObject robot1;
    private double dAxe3_t;

    void Start()
    {
        robot = GameObject.Find("robot");
        robot1 = GameObject.Find("robot1");
    }

    void Update()
    {
        dAxe3_t = ((double)(this.robot1.transform.rotation.eulerAngles.z));
    }

}
