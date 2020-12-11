using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public float maxSpeed = 10.0f;
    public float turnSpeed = 1.0f;
    public float maxUpRotation = 90.0f;
    public float upRotationSpeed = 1.0f;

    private float currentSpeed = 0.0f;
    private float currentUpRotation = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Translate
        Vector3 forward_world = transform.TransformDirection(Vector3.forward);
        if (Input.GetKey(KeyCode.W))
            currentSpeed = Mathf.Min(maxSpeed, currentSpeed + maxSpeed / 100.0f);
        else if (Input.GetKey(KeyCode.S))
            currentSpeed = Mathf.Max(-maxSpeed, currentSpeed - maxSpeed / 100.0f);
        else
            currentSpeed *= 0.9f;
        gameObject.GetComponent<CharacterController>().Move(forward_world * currentSpeed * Time.deltaTime);

        // Rotate
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.up, turnSpeed);
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(Vector3.up, -turnSpeed);

        // Camera cam = gameObject.GetComponent<Camera>();
        float oldUpRotation = currentUpRotation;
        if (Input.GetKey(KeyCode.Space))
            currentUpRotation = Mathf.Min(currentUpRotation + upRotationSpeed, maxUpRotation);
        else
            currentUpRotation = Mathf.Max(currentUpRotation - 10.0f * upRotationSpeed, 0.0f);

        GameObject cam = GameObject.Find("Main Camera");
        cam.transform.Rotate(Vector3.left, currentUpRotation - oldUpRotation);

    }
}
