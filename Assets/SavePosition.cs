using UnityEngine;

public class SavePosition : MonoBehaviour
{
    // Generate a unique ID for this GameObject.
    public string guid = System.Guid.NewGuid().ToString();

    void Awake()
    {
        transform.localPosition = ES3.Load<Vector3>(guid, transform.localPosition);
    }

    void OnDestroy()
    {
        ES3.Save<Vector3>(guid, transform.localPosition);
    }

    void Update()
    {
        transform.localPosition = ES3.Load<Vector3>(guid, transform.localPosition);
    }
}
