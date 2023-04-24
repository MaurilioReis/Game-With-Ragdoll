using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{
    Animator anim;
    CapsuleCollider colBase;
    SphereCollider colTriggerPile;
    Rigidbody[] rbs;
    Rigidbody rbHip;

    bool inPile = false;
    Transform posPile;
    void Start()
    {
        anim = GetComponent<Animator>();
        colBase = GetComponent<CapsuleCollider>();
        colTriggerPile = GetComponent<SphereCollider>();
        rbs = gameObject.GetComponentsInChildren<Rigidbody>();
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

        colTriggerPile.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PileController scriptPile = other.GetComponent<PileController>();
            if (scriptPile.pileBodies.Count < scriptPile.limitPile)
            {
                posPile = scriptPile.AddToPile();
                TakeBodyToPile();
            }
        }
    }

    public void TakeBodyToPile()
    {
        foreach (Rigidbody allRb in rbs)
        {
            allRb.gameObject.layer = 6; // layer ignore collision player
        }

        rbHip = transform.GetChild(4).GetComponent<Rigidbody>(); // get rb object hip
        rbHip.useGravity = false;
        rbHip.isKinematic = true;

        inPile = true;
    }

    private void Update()
    {
        if (inPile == true)
        {
            if (rbHip.transform.localPosition != Vector3.zero)
            {
                rbHip.transform.localPosition = Vector3.Lerp(rbHip.transform.localPosition, Vector3.zero, 5 * Time.deltaTime);
            }

            if (transform.position != posPile.position)
            {
                transform.position = Vector3.Lerp(transform.position, posPile.position, 5 * Time.deltaTime);
            }
            else
            {
                transform.SetParent(posPile);
            }

        }
    }

    public void DropSacrifice()
    {

    }
}
