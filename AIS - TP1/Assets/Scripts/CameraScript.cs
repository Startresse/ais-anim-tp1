using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float cameraStrength = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 center = transform.parent.transform.position;
        transform.RotateAround(center, Vector3.up, cameraStrength * Input.GetAxis("Mouse X"));
    }
}
