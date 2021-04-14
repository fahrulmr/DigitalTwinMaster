using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Arm_Controller : MonoBehaviour
{
    public Slider baseSlider;
    public Slider armSlider;
    

    // slider value for base platform that goes from -1 to 1.
    public float baseSliderValue = 1.0f;

    // slider value for upper arm that goes from -1 to 1.
    public float upperArmSliderValue = 1.0f;
    public float j3value = 1.0f;
    public float j4value = 1.0f;
    public float j5value = 1.0f;
    public float j6value = 1.0f;

    // These slots are where you will plug in the appropriate arm parts into the inspector.
    public Transform robotBase;
    public Transform upperArm;
    public Transform j3;
    public Transform j4;
    public Transform j5;
    public Transform j6;

    // Allow us to have numbers to adjust in the inspector the speed of each part's rotation.
    public float baseTurnRate = 1.0f;
    public float upperArmTurnRate = 1.0f;
    public float j3TurnRate = 1.0f;
    public float j4TurnRate = 1.0f;
    public float j5TurnRate = 1.0f;
    public float j6TurnRate = 1.0f;

    private float baseYRot = 0.0f;
    public float baseYRotMin = 0.0f;
    public float baseYRotMax = 0.0f;

    private float upperArmXRot = 0.0f;
    public float upperArmXRotMin = -45.0f;
    public float upperArmXRotMax = 45.0f;

    private float j3ArmXRot = 0.0f;
    public float j3ArmXRotMin = -45.0f;
    public float j3ArmXRotMax = 45.0f;

    private float j4ArmXRot = 0.0f;
    public float j4ArmXRotMin = -45.0f;
    public float j4ArmXRotMax = 45.0f;

    private float j5ArmXRot = 0.0f;
    public float j5ArmXRotMin = -45.0f;
    public float j5ArmXRotMax = 45.0f;

    private float j6ArmXRot = 0.0f;
    public float j6ArmXRotMin = -45.0f;
    public float j6ArmXRotMax = 45.0f;

    [System.Serializable]
    public class fanucdata
    {
        public float Joint1;
        public float Joint2;
        public float Joint3;
        public float Joint4;
        public float Joint5;
        public float Joint6;
    }
        void Start()
    {   
       
        /* Set default values to that we can bring our UI sliders into negative values */
        baseSlider.minValue = -360;
        //armSlider.minValue = -1;
        baseSlider.maxValue = 360;
       // armSlider.maxValue = 1;
    }

    public void LoadFromJsonFanuc()
    {
        //string json = File.ReadAllText(Application.dataPath + "/fanucdatafrombackend4.json");
        // fanucdata data = JsonUtility.FromJson<fanucdata>(json);
        string json = File.ReadAllText(Application.dataPath + "/fanucdatafrombackend5.json");
        fanucdata data = JsonUtility.FromJson<fanucdata>(json);
        baseYRotMax = data.Joint1;
        
        upperArmXRotMax = data.Joint2;
        j3ArmXRotMax = data.Joint3;
        j4ArmXRotMax = data.Joint4;
        j5ArmXRotMax = data.Joint5;
        j6ArmXRotMax = data.Joint6;


    }
    void CheckInput()
    {
       // baseSliderValue = baseSlider.value;
        //upperArmSliderValue = armSlider.value;
    }
    void ProcessMovement()
    {
        //rotating our base of the robot here around the Y axis and multiplying
        //the rotation by the slider's value and the turn rate for the base.
        baseYRot += baseSliderValue * baseTurnRate;
        baseYRot = Mathf.Clamp(baseYRot, baseYRotMin, baseYRotMax);
        robotBase.localEulerAngles = new Vector3(robotBase.localEulerAngles.x, baseYRot, robotBase.localEulerAngles.z);
        
        

        //rotating our upper arm of the robot here around the X axis and multiplying
        //the rotation by the slider's value and the turn rate for the upper arm.
        upperArmXRot += upperArmSliderValue * upperArmTurnRate;
        upperArmXRot = Mathf.Clamp(upperArmXRot, upperArmXRotMin, upperArmXRotMax);
        upperArm.localEulerAngles = new Vector3(upperArm.localEulerAngles.x, upperArm.localEulerAngles.y, upperArmXRot);

        j3ArmXRot += j3value * j3TurnRate;
        j3ArmXRot = Mathf.Clamp(j3ArmXRot, j3ArmXRotMin, j3ArmXRotMax);
        j3.localEulerAngles = new Vector3(j3.localEulerAngles.x, j3.localEulerAngles.y, j3ArmXRot);

        j4ArmXRot += j4value * j4TurnRate;
        j4ArmXRot = Mathf.Clamp(j4ArmXRot, j4ArmXRotMin, j4ArmXRotMax);
        //j4.localEulerAngles = new Vector3( j4ArmXRot, j4.localEulerAngles.y ,j4.localEulerAngles.z);
        j4.localRotation =  Quaternion.Euler(j4ArmXRot, j4.localEulerAngles.y, j4.localEulerAngles.z);

        j5ArmXRot += j5value * j5TurnRate;
        j5ArmXRot = Mathf.Clamp(j5ArmXRot, j5ArmXRotMin, j5ArmXRotMax);
        j5.localEulerAngles = new Vector3(j5.localEulerAngles.x, j5.localEulerAngles.y, j5ArmXRot);

        j6ArmXRot += j6value * j6TurnRate;
        j6ArmXRot = Mathf.Clamp(j6ArmXRot, j6ArmXRotMin, j6ArmXRotMax);
        //j4.localEulerAngles = new Vector3( j4ArmXRot, j4.localEulerAngles.y ,j4.localEulerAngles.z);
        j6.localRotation = Quaternion.Euler(j6ArmXRot, j6.localEulerAngles.y, j6.localEulerAngles.z);

    }


    public void ResetSliders()
    {
        //resets the sliders back to 0 when you lift up on the mouse click down (snapping effect)
        //baseSliderValue = 0.0f;
        //upperArmSliderValue = 0.0f;
        //baseSlider.value = 0.0f;
        //armSlider.value = 0.0f;
    }



    void Update()
    {
        CheckInput();
        ProcessMovement();
        //ResetSliders();
        LoadFromJsonFanuc();
    }
}
