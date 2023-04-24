using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{
    Animator anim;
    CapsuleCollider colBase;
    Rigidbody[] rbs;
    void Start()
    {
        anim = GetComponent<Animator>();
        colBase = GetComponent<CapsuleCollider>();
        rbs = gameObject.GetComponentsInChildren<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            ReceiveAtack(transform, 1);
        }
    }

    public void ReceiveAtack(Transform directionImpact, float forceImpact)
    {
        colBase.enabled = false;

        foreach(Rigidbody allRb in rbs)
        {
            allRb.useGravity = true;
            allRb.isKinematic = false;
            allRb.AddForce(directionImpact.forward * forceImpact, ForceMode.Impulse);
        }

        anim.enabled = false;
    }

    public void GetBody()
    {

    }

    public void DropSacrifice()
    {

    }
}
