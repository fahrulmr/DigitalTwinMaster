using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonReader3D : MonoBehaviour
{
    public TextAsset textJSON;
    // Start is called before the first frame update

    [System.Serializable]
    public class Player_j2
    {
        public int current_pos_j2;
        public int world_pos_y;
        public int world_pos_z;
        
    }

    [System.Serializable]

    public class Playerlist
    {
        public Player_j2[] player;
    }

    public Playerlist myPlayerList = new Playerlist();
    void Start()
    {
        myPlayerList = JsonUtility.FromJson<Playerlist>(textJSON.text);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
