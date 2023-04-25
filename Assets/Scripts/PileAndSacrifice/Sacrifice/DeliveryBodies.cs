using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryBodies : MonoBehaviour
{
    bool activeSystem;
    public PileController pileController;
    public Image fillBar;
    public float speedFill = 0.1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && activeSystem == false)
        {
            activeSystem = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && activeSystem == true)
        {
            activeSystem = false;
            fillBar.fillAmount = 0;
        }
    }

    void Update()
    {
        if (activeSystem == true && pileController.pileBodies.Count > 0)
        {
            if (fillBar.fillAmount < 1)
            {
                fillBar.fillAmount += speedFill * Time.deltaTime;
            }
            else
            {
                int valueLastPile = pileController.pileBodies.Count;
                valueLastPile--;
                GameObject lastSlotPile = pileController.pileBodies[pileController.pileBodies.Count - 1].gameObject;
                BodyController scriptBodySacrifice = lastSlotPile.transform.GetChild(1).gameObject.GetComponent<BodyController>();

                scriptBodySacrifice.DropSacrifice();
                fillBar.fillAmount = 0;

                Destroy(lastSlotPile.gameObject);
                pileController.pileBodies.RemoveAt(valueLastPile);
            }
        }
    }
}
