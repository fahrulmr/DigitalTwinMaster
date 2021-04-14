using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class RotateWithSlider : MonoBehaviour
{
    // Assign in the inspector
    public GameObject objectToRotate;
    public Slider slider;
    

    // Preserve the original and current orientation
    public float previousValue;

    void Awake()
    {
        slider.minValue = -360;
        slider.maxValue = 360;
        // Assign a callback for when this slider changes
        this.slider.onValueChanged.AddListener(this.OnSliderChanged);

        // And current value
        this.previousValue = this.slider.value;
    }

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
    public void LoadFromJsonFanuc()
    {
        //string json = File.ReadAllText(Application.dataPath + "/fanucdatafrombackend4.json");
        // fanucdata data = JsonUtility.FromJson<fanucdata>(json);
        string json = File.ReadAllText(Application.dataPath + "/fanucdatafrombackend4.json");
        fanucdata data = JsonUtility.FromJson<fanucdata>(json);
        //previousValue = data.Joint1;
        


    }
    void OnSliderChanged(float value)
    {
        // How much we've changed
        float delta = value - this.previousValue;
        this.objectToRotate.transform.Rotate(Vector3.right * delta * 360);

        // Set our previous value for the next change
        this.previousValue = value;
    }

    void Update()
    {
        LoadFromJsonFanuc();
    }

}