using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class JsonReadWriteSystem : MonoBehaviour
{
    public float speed = 5f;

    // public InputField idInputField;
    // public InputField nameInputField;
    //  public InputField infoInputField;
    //  public GameObject gesercube;

    //public Text J2Text;
    // public Text J2PosY;
    // public Text J2PosZ;

    //public Transform Fanuc;
    
    //public GameObject cmm;

    public Transform J1;
    public Transform J2;
    public Transform J3;
    public Transform J4;
    public Transform J5;
    public Transform J6;

    public GameObject magazine_ismoving;

    public GameObject Grinding_is_working;
    public GameObject Lasermarking_is_working;
    public GameObject polishing_is_working;
    // public GameObject cmm_mc_access;
    // public GameObject cnc_isopened;

    public Animator CNC; 
    public bool cncbuka;

    public Animator railway;
    public bool railwaymove;
    
    [System.Serializable]
    public class fanucdata
    {
        //  public string Id;
        //   public string Name;
        //  public string Information;
        // public int Cube;
        public float Joint1;
        public float Joint2;
        public float Joint3;
        public float Joint4;
        public float Joint5;
        public float Joint6;

        public float world_pos_y;
        public float world_pos_z;

        
        public float user_frame_y;
        public float user_frame_w;

        public bool mc_access;
        public string chuck5;
        public string unlock_chuck;

        public bool has_reached;
        public bool is_moving ;
        public bool destination;
        

        public bool is_opened;
        public bool is_closed;

        public bool is_working;
    }
    public class railwayclass {

        public int destination;
        public bool is_moving;
    }

    //public void SaveToJson()
    //{
    //WeaponData data = new WeaponData();
    // data.Id = idInputField.text;
    // data.Name = nameInputField.text;
    // data.Information = infoInputField.text;



    //     string json = JsonUtility.ToJson(data, true);
    //    File.WriteAllText(Application.dataPath + "/WeaponDataFile.json", json);

    // }


    public void Start()
    {
        // isMoving = GetComponent<Animator>();
        CNC = GetComponent<Animator>();
        



    }
    public void LoadFromJsonFanuc()
    {
        string json = File.ReadAllText(Application.dataPath + "/fanucdatafrombackend4.json");
        fanucdata data = JsonUtility.FromJson<fanucdata>(json);

        //Fanuc.position = new Vector3(data.current_pos_j1, 0,0);


        // this is for rotation object
        // Fanuc.rotation = Quaternion.EulerRotation(90f,0f,0f);
        J1.rotation = Quaternion.Euler(data.Joint1, 0, 0);
        J2.rotation = Quaternion.Euler(0, data.Joint2, 0);
        J3.rotation = Quaternion.Euler(0, 0, data.Joint3);
        J4.rotation = Quaternion.Euler(data.Joint4, 0, 0);
        J5.rotation = Quaternion.Euler(0, data.Joint5, 0);
        J6.rotation = Quaternion.Euler(0, 0, data.Joint6);







        //Fanuc.rotation
        // idInputField.text = data.Id;
        //nameInputField.text = data.Name;
        // infoInputField.text = data.Information;
    }

    /*public void LoadFromJsonCmm()
    {
        string json = File.ReadAllText(Application.dataPath + "/cmmdata.json");
        fanucdata data = JsonUtility.FromJson<fanucdata>(json);

        if (data.mc_access == true)
        {

            cmm_mc_access.gameObject.GetComponent<Animator>().SetBool("mc_access",true);
            //magazine_ismoving.gameObject.GetComponent<Animator>().speed[];

        }

    }
    */
    public void LoadFromJsonMagazine()
    {
        string json = File.ReadAllText(Application.dataPath + "/magazinedata.json");
        fanucdata data = JsonUtility.FromJson<fanucdata>(json);
        


        if (data.is_moving == true)
        {

            magazine_ismoving.gameObject.GetComponent<Animator>().enabled = true;
            //magazine_ismoving.gameObject.GetComponent<Animator>().speed[];

        }
        if (data.is_moving == false)
        {

            magazine_ismoving.gameObject.GetComponent<Animator>().enabled = false;

        }


    }

        public void LoadFromJsonCnc()
        {
            string json = File.ReadAllText(Application.dataPath + "/cncdata.json");
            fanucdata data = JsonUtility.FromJson<fanucdata>(json);

            CNC.SetBool("buka", cncbuka);

            if (data.is_opened == true)
            {
                cncbuka = true;
                // cnc_isopened.gameObject.GetComponent<Animator>().SetBool("buka",true);
                //magazine_ismoving.gameObject.GetComponent<Animator>().speed[];

            }
            if (data.is_opened == false)
            {
                cncbuka = false;
                // cnc_isopened.gameObject.GetComponent<Animator>().SetBool("buka",true);
                //magazine_ismoving.gameObject.GetComponent<Animator>().speed[];

            }
        }
        public void LoadFromJsonGrinding()
        {
            string json = File.ReadAllText(Application.dataPath + "/grindingpolishingdata.json");
            fanucdata data = JsonUtility.FromJson<fanucdata>(json);



            if (data.is_working == true)
            {

                Grinding_is_working.gameObject.GetComponent<Animator>().enabled = true;
                //magazine_ismoving.gameObject.GetComponent<Animator>().speed[];

            }
            if (data.is_working == false)
            {

               Grinding_is_working.gameObject.GetComponent<Animator>().enabled = false;

            }


        }
        public void LoadFromJsonLaserMarking()
        {
            string json = File.ReadAllText(Application.dataPath + "/lasermarkingdata.json");
            fanucdata data = JsonUtility.FromJson<fanucdata>(json);



            if (data.is_working == true)
            {

                Lasermarking_is_working.gameObject.GetComponent<Animator>().enabled = true;
            Debug.Log("lasermarking OK");
                //magazine_ismoving.gameObject.GetComponent<Animator>().speed[];

            }
            if (data.is_working == false)
            {

               Lasermarking_is_working.gameObject.GetComponent<Animator>().enabled = false;

            }


        }
    public void LoadFromJsonPolishing()
    {
        string json = File.ReadAllText(Application.dataPath + "/polishingdata.json");
        fanucdata data = JsonUtility.FromJson<fanucdata>(json);



        if (data.is_working == true)
        {

            polishing_is_working.gameObject.GetComponent<Animator>().enabled = true;
            Debug.Log("polishing OK");
            //magazine_ismoving.gameObject.GetComponent<Animator>().speed[];

        }
        if (data.is_working == false)
        {

            polishing_is_working.gameObject.GetComponent<Animator>().enabled = false;

        }


    }

    public void LoadFromJsonRailway()
    {
        string json = File.ReadAllText(Application.dataPath + "/railwaydata.json");
        railwayclass data = JsonUtility.FromJson<railwayclass>(json);

        //railway.SetInteger ("ke1",railwaymove)

        if (data.destination == 1 )
        {
            railway.Play("ke1");
            // cnc_isopened.gameObject.GetComponent<Animator>().SetBool("buka",true);
            //magazine_ismoving.gameObject.GetComponent<Animator>().speed[];

        }
        if (data.destination == 2)
        {
            railway.Play("ke2");
            // cnc_isopened.gameObject.GetComponent<Animator>().SetBool("buka",true);
            //magazine_ismoving.gameObject.GetComponent<Animator>().speed[];

        }
        if (data.destination == 3)
        {
            railway.Play("ke3");
            // cnc_isopened.gameObject.GetComponent<Animator>().SetBool("buka",true);
            //magazine_ismoving.gameObject.GetComponent<Animator>().speed[];

        }
    }




    void Update()
    {
        //LoadFromJsonFanuc();
        //LoadFromJsonCmm();
        LoadFromJsonMagazine();
        LoadFromJsonCnc();
        LoadFromJsonGrinding();
       // LoadFromJsonLaserMarking();
        LoadFromJsonPolishing();
        //LoadFromJsonRailway();
    }
}

