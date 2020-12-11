using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public float speed = 1.0f;
    public float rotationSpeed = 1.0f;

    private float currentSpeed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Translate
        Vector3 forward_world = transform.TransformDirection(Vector3.forward);
        currentSpeed = Mathf.Min(speed, currentSpeed + speed / 100.0f);
        gameObject.GetComponent<CharacterController>().Move(forward_world * currentSpeed * Time.deltaTime);



    }
}
