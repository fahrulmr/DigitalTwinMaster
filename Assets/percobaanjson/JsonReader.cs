using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    public TextAsset textJSON;
    // Start is called before the first frame update

    [System.Serializable]
    public class Player
    {public string name;
    public int health;
    public int mana;
    }

    [System.Serializable]

    public class Playerlist
    {
        public Player[] player;
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
