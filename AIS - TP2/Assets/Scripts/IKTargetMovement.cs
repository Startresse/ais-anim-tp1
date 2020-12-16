using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTargetMovement : MonoBehaviour
{

    public float speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            transform.position += Vector3.left * speed;
        if (Input.GetKey(KeyCode.D))
            transform.position += Vector3.right * speed;
        if (Input.GetKey(KeyCode.W))
            transform.position += Vector3.forward * speed;
        if (Input.GetKey(KeyCode.S))
            transform.position += Vector3.back* speed;
        if (Input.GetKey(KeyCode.Space))
            transform.position += Vector3.up* speed;
        if (Input.GetKey(KeyCode.LeftShift))
            transform.position += Vector3.down* speed;
    }
}
