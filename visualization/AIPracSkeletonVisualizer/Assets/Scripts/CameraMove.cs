using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public float distance;
    public float height;

    private void Start()
    {
        transform.position = new Vector3(distance, height, 0);
    }

    void Update()
    {
        /*float x = Mathf.Sin(Time.time) * distance;
        float z = Mathf.Cos(Time.time) * distance;
        transform.position = new Vector3(x, height, z);*/
        transform.LookAt(target.position);
        transform.RotateAround(target.position, Vector3.up, 20f * Time.deltaTime);

    }
}