using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            anim.SetBool("Move", true);
        }    
        else
        {
            anim.SetBool("Move", false);
        }
    }
}
