using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement_script : MonoBehaviour
{
    // since its on crab, "gameObject" auto targets self (crab)
    Rigidbody rb;  // rigidbody is how u simulate physics in engine
    Animator anim; //This is to get a reference to our Animator Component

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // these horizontal and vertical axes are set up by default in Unity. So can access this way
        float horizontal = Input.GetAxis("Horizontal")*5;
        float vertical = Input.GetAxis("Vertical")*2;

        if(horizontal == 0 && vertical == 0)
        {
            anim.SetBool("isMoving", false);
        }
        else
        {
            anim.SetBool("isMoving", true);
        }

        rb.velocity = new Vector3(horizontal, 0, vertical);
    }
}
