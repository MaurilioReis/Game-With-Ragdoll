using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSetCam : MonoBehaviour
{
    public CamConfig camController;
    
    [Header("SET DIRECTION CAM:   0 = foward, 1 = left, 2 = right")]
    [Range(0,2)]
    public int setCam;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            camController.typeCam = setCam;
        }
    }
}
