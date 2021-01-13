using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKInterpolCylinder : MonoBehaviour
{
    public Transform root;
    public Transform tail;

    void Start()
    {
        // Scale
        Vector3 direction = tail.position - root.position;
        float mag = direction.magnitude;
        transform.localScale = new Vector3(0.5f, mag / 2.0f, 0.5f);
    }

    void Update()
    {
        // Move
        transform.position = (root.position + tail.position)/2.0f;

        // Rotate
        transform.up = (tail.position - root.position).normalized;
    }
}
