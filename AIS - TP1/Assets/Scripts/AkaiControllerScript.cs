using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkaiControllerScript : MonoBehaviour
{
    Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }


    private float v = 0f;
    public float accel = 1.0f;
    public float decel = 1.0f;
    private float smoothed() {
        float accelerating = Input.GetAxisRaw("Vertical");
        if (accelerating > 0)
            v = Mathf.Clamp(v + accel * Time.deltaTime, -1f, 1f);
        else if (accelerating < 0)
            v = Mathf.Clamp(v - accel * Time.deltaTime, -1f, 1f);
        else {
            if ( v > 0 )
                v = Mathf.Clamp(v - decel * Time.deltaTime, 0f, 1f);
            else
                v = Mathf.Clamp(v + decel * Time.deltaTime, -1f, 0f);
        }
        return v;
    }

    // Update is called once per frame
    void Update()
    {
        myAnimator.SetBool("isJumping", Input.GetKey(KeyCode.Space));
        myAnimator.SetFloat("vSpeed", smoothed());
        myAnimator.SetFloat("hSpeed", Input.GetAxis("Horizontal"));
    }
}
