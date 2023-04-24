using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileController : MonoBehaviour
{
    public GameObject gameObjectPile; 
    public List<Transform> pileBodies;
    public GameObject emptySlot;

    Transform lastTransformPile;

    public int limitPile = 1;

    public Transform AddToPile()
    {
        if (pileBodies.Count <= 0)
        {
            Vector3 offsetSlotY = new Vector3(0, 0.47f, 0);

            GameObject newSlot = Instantiate(emptySlot, gameObjectPile.transform.position + offsetSlotY, gameObjectPile.transform.rotation);
            newSlot.name = "Slot 1";

            FixedJoint joint = newSlot.gameObject.GetComponent<FixedJoint>();
            joint.connectedBody = GetComponent<Rigidbody>();

            pileBodies.Add(newSlot.transform);
            lastTransformPile = newSlot.transform;
        }
        else
        {
            Vector3 positionSlot = lastTransformPile.GetChild(0).transform.position;

            GameObject newSlot = Instantiate(emptySlot, positionSlot, lastTransformPile.rotation);
            newSlot.name = "Slot " + (pileBodies.Count + 1);

            FixedJoint joint = newSlot.gameObject.GetComponent<FixedJoint>();
            joint.connectedBody = lastTransformPile.gameObject.GetComponent<Rigidbody>();

            pileBodies.Add(newSlot.transform);
            lastTransformPile = newSlot.transform;
        }

        return lastTransformPile;
    }
}
