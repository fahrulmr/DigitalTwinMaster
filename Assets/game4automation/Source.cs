// Game4Automation (R) Framework for Automation Concept Design, Virtual Commissioning and 3D-HMI
// (c) 2019 in2Sight GmbH - Usage of this source code only allowed based on License conditions see https://game4automation.com/lizenz  

using System.Collections.Generic;
using UnityEngine;

namespace game4automation
{
    [SelectionBase]
    //! The Source is generating MUs during simulation time.
    //! The Source is generating new MUs based on the referenced (ThisObjectAsMU) GameObject. 
    //! When generating an MU a copy of the referenced GameObject will be created.
    [HelpURL("https://game4automation.com/documentation/current/source.html")]
    public class Source : BaseSource
    {
        // Public / UI Variablies
        [Header("General Settings")] public GameObject ThisObjectAsMU; //!< The referenced GameObject which should be used as a prototype for the MU. If it is null it will be this GameObject.
        public GameObject Destination; //!< The destination GameObject where the generated MU should be placed
        public bool Enabled = true; //!< If set to true the Source is enabled
        public bool FreezeSourcePosition = true; //!< If set to true the Source itself (the MU template) is fixed to its position
        public bool DontVisualize = true; //!< True if the Source should not be visible during Simulation time
        public float Mass = 1; //!< Mass of the generated MU.
        public string GenerateOnLayer ="g4a SensorMU"; //! Layer where the MUs should be generated to

        [Header("Create in Inverval (0 if not)")]
        public float StartInterval = 0; //! Start MU creation with the given seconds after simulation start
        public float Interval = 0; //! Interval in seconds between the generation of MUs. Needs to be set to 0 if no interval generation is wished.

        [Header("Automatic Generation on Distance")]
        public bool AutomaticGeneration = true; //! Automatic generation of MUs if last MU is above the GenerateIfDistance distance from MU
        public float GenerateIfDistance = 300; //! Distance in millimeters from Source when new MUs should be generated.

        [Header("Number of MUs")] public bool LimitNumber = false;
        public int MaxNumberMUs = 1;
        [ReadOnly]public int Created = 0;
        
        [Header("Source IO's")] public bool GenerateMU; //! When changing from false to true a new MU is generated.
        public bool DeleteAllMU; //! when changing from false to true all MUs generated by this Source are deleted.

        [Header("Source Signals")] public PLCOutputBool SourceGenerate; //! When changing from false to true a new MU is generated.
        
        // Private Variablies
        private bool _generatebefore = false;
        private bool _deleteallmusbefore = false;
        private bool _tmpoccupied;
        private GameObject _lastgenerated;
        private int ID = 0;
        private bool _generatenotnull = false;
        private List<GameObject> _generated = new List<GameObject>();

        protected void Reset()
        {
            if (ThisObjectAsMU == null)
            {
                ThisObjectAsMU = gameObject;
            }
        }

        protected void Start()
        {
            if (SourceGenerate != null)
                _generatenotnull = true;
            
            if (ThisObjectAsMU == null)
            {
                ErrorMessage("Object to be created needs to be defined in [This Object As MU]");
            }

            if (ThisObjectAsMU != null)
            {
                if (ThisObjectAsMU.GetComponent<MU>() == null)
                {
                    ErrorMessage(
                        "Object [" + ThisObjectAsMU.name + "] which is acting as MU needs MU script attached to it");
                }
            }

            if (Interval > 0)
            {
                InvokeRepeating("Generate", StartInterval, Interval);
            }

            SetVisibility(!DontVisualize);
            SetCollider(false);
            SetFreezePosition(FreezeSourcePosition);

            if (GetComponent<Collider>() != null)
            {
                GetComponent<Collider>().enabled = false;
            }
        }


        private void Update()
        {
            if (_generatenotnull)
                GenerateMU = SourceGenerate.Value;

            // Generate on Signal Genarate MU
            if (_generatebefore != GenerateMU)
            {
                if (GenerateMU)
                {
                    _generatebefore = GenerateMU;
                    Generate();
                }
            }

            // Generate if Distance
            if (AutomaticGeneration)
            {
                if (_lastgenerated != null)
                {
                    float distance = Vector3.Distance(_lastgenerated.transform.position, gameObject.transform.position) *
                                     Game4AutomationController.Scale;

                    if (distance > GenerateIfDistance)
                    {
                        Generate();
                    }
                }
            }

            // Generate on Keypressed
            if (Input.GetKeyDown(Game4AutomationController.HotkeyCreateOnSource))
            {
                Generate();
            }

            if (GenerateMU == false)
            {
                _generatebefore = false;
            }

            if (DeleteAllMU != _deleteallmusbefore && DeleteAllMU == true)
            {
                DeleteAll();
            }


            // Delete  on Keypressed
            if (Input.GetKeyDown(Game4AutomationController.HotkeyDelete))
            {
                if (Game4AutomationController.EnableHotkeys)
                    DeleteAll();
            }


            _deleteallmusbefore = DeleteAllMU;
        }

        //! Generates an MU.
        public void Generate()
        {
            if (LimitNumber && (Created >= MaxNumberMUs))
                return;
            
            if (Enabled)
            {
                GameObject newmu = GameObject.Instantiate(ThisObjectAsMU, transform.position, transform.rotation);
                if (LayerMask.NameToLayer(GenerateOnLayer) != -1)
                {
                    newmu.layer = LayerMask.NameToLayer(GenerateOnLayer);
                }

                Source source = newmu.GetComponent<Source>();

                Created++;
                
                Rigidbody newrigid = newmu.GetComponent<Rigidbody>();
                if (newrigid == null)
                {
                    newrigid = newmu.AddComponent<Rigidbody>();
                }

                newrigid.interpolation = RigidbodyInterpolation.Interpolate;
                newrigid.mass = Mass;
                
                BoxCollider newboxcollider = newmu.GetComponentInChildren<BoxCollider>();
                if (newboxcollider == null)
                {
                    newboxcollider = newmu.AddComponent<BoxCollider>();
                    MeshFilter mumsmeshfilter = newmu.GetComponentInChildren<MeshFilter>();
                    Mesh mumesh = mumsmeshfilter.mesh;
                    GameObject obj = mumsmeshfilter.gameObject;
                    if (mumesh != null)
                    {
                        Vector3 globalcenter = obj.transform.TransformPoint(mumesh.bounds.center);
                        Vector3 globalsize = obj.transform.TransformVector(mumesh.bounds.size);
                        newboxcollider.center = newmu.transform.InverseTransformPoint(globalcenter);
                        Vector3 size = newmu.transform.InverseTransformVector(globalsize);
                        if (size.x < 0)
                        {
                            size.x = -size.x;
                        }

                        if (size.y < 0)
                        {
                            size.y = -size.y;
                        }

                        if (size.z < 0)
                        {
                            size.z = -size.z;
                        }

                        newboxcollider.size = size;
                    }
                }
                else
                {
                    newboxcollider.enabled = true;
                }

                newrigid.mass = Mass;
                source.SetVisibility(true);
                source.SetCollider(true);
                source.SetFreezePosition(false);

                source.Enabled = false;
                source.enabled = false;
                ID++;
                MU mu = newmu.GetComponent<MU>();
                if (Destination != null)
                {
                    newmu.transform.parent = Destination.transform;
                }
            
                if (mu == null)
                {
                    ErrorMessage("Object generated by source need to have MU script attached!");
                }
                else
                {
                    var name = ThisObjectAsMU.name + "-" + ID.ToString();
                    mu.InitMu(name,ID,Game4AutomationController.GetMUID(newmu));

                }

                Destroy(source);
            
                _lastgenerated = newmu;
                _generated.Add(newmu);
            }
        }
        
        //! Deletes all MU generated by this Source
        public void DeleteAll()
        {
            foreach (GameObject obj in _generated)
            {
                Destroy(obj);
            }

            _generated.Clear();
        }
    }
}