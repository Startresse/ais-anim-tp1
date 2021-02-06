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
        float up_down_mvt = 0f;
        if (Input.GetKey(KeyCode.Space))
            up_down_mvt += 1f;
        if (Input.GetKey(KeyCode.LeftShift))
            up_down_mvt -= 1f;

        transform.position += speed * new Vector3(
            Input.GetAxisRaw("Horizontal"),
            up_down_mvt,
            Input.GetAxisRaw("Vertical")
        );
    }
}
