using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class railwaymove : MonoBehaviour
{
    public Transform target;
    public float speed = 1f;
    public int n = 1;



    [System.Serializable]
    public class railwaydata
    { 
    public float destination;
    }
    void Update()
    {
        float move = speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, target.position, move);
        transform.position = Vector3.Lerp(transform.position, target.position, n * Time.deltaTime);
        LoadFromJsonRailway();
    }

    public void LoadFromJsonRailway()
    {
        string json = File.ReadAllText(Application.dataPath + "/railwaydata1.json");
        railwaydata data = JsonUtility.FromJson<railwaydata>(json);

        //CNC.SetBool("buka", cncbuka);
        if (data.destination == 1)
        {
            target.position = new Vector3(0.177f, 0.085f, -0.153f);
        }
        if (data.destination == 2)
        {
            target.position = new Vector3(-0.606f, 0.085f, -0.153f);
        }
        if (data.destination == 3)
        {
            target.position = new Vector3(-0.968f, 0.085f, -0.153f);
        }
        if (data.destination == 4)
        {
            target.position = new Vector3(-1.031f, 0.085f, -0.153f);
        }
        
    }
}
