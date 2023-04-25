using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    Animator anim;
    CapsuleCollider colBase;
    SphereCollider colTriggerPile;
    Rigidbody[] rbs;
    Rigidbody rbHip;

    bool isPilingUp = false;
    Quaternion rotInPile = new Quaternion(90, 0, 0, 0);
    Transform posPile;
    float speedAproximity = 0.1f;

    Transform sacrificePosition;
    bool isSacrificing;
    void Start()
    {
        anim = GetComponent<Animator>();
        colBase = GetComponent<CapsuleCollider>();
        colTriggerPile = GetComponent<SphereCollider>();
        rbs = gameObject.GetComponentsInChildren<Rigidbody>();
        sacrificePosition = GameObject.FindGameObjectWithTag("PosSacrifice").GetComponent<Transform>();
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

        StartCoroutine("waitToActiveTriggerPile");
    }

    IEnumerator waitToActiveTriggerPile()
    {
        yield return new WaitForSeconds(3);

        colTriggerPile.enabled = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            PileController scriptPile = other.GetComponent<PileController>();
            if (scriptPile.pileBodies.Count < scriptPile.limitPile)
            {
                colTriggerPile.enabled = false;

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

        isPilingUp = true;
    }

    public void DropSacrifice()
    {
        isPilingUp = false;
        isSacrificing = true;
        transform.SetParent(null);
        speedAproximity = 0.1f;
    }

    private void Update()
    {
        if (isPilingUp == true)
        {
            if (rbHip.transform.localPosition != Vector3.zero)
            {
                rbHip.transform.localPosition = Vector3.Lerp(rbHip.transform.localPosition, Vector3.zero, speedAproximity * Time.deltaTime);
            }

            float distanceSlot = Vector3.Distance(transform.position, posPile.position);

            if (distanceSlot > 0.15f)
            {
                transform.position = Vector3.Lerp(transform.position, posPile.position, speedAproximity * Time.deltaTime);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, rotInPile, 15 * Time.deltaTime);
                speedAproximity += 0.2f;
            }
            else
            {
                transform.SetParent(posPile);
                isPilingUp = false;
            }
        }
        else if (isSacrificing == true)
        {
            float distanceSacrifice = Vector3.Distance(transform.position, sacrificePosition.position);

            if (distanceSacrifice > 0.1f)
            {
                transform.position = Vector3.Lerp(transform.position, sacrificePosition.position, speedAproximity * Time.deltaTime);
                speedAproximity += 0.01f;
            }
            else
            {
                colBase.enabled = true;
                isSacrificing = false;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                this.enabled = false;
            }
        }
    }
}
