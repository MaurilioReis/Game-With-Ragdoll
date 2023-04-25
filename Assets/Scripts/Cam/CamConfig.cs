using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamConfig : MonoBehaviour
{
    public int typeCam; // 0 foward, 1 Left, 2 Right, 3 Speed, 4 Capacity
    public float speedRot;

    public Quaternion rot0;
    public Quaternion rot1;
    public Quaternion rot2;

    [Space(10)]
    public PileController pile;
    Transform playerPos;
    Transform cam;
    public float distance;

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;   
    }


    public void Update()
    {
        if (typeCam == 0 && (int)transform.rotation.eulerAngles.y != (int)rot0.y)
        {
            transform.rotation =   Quaternion.Lerp(transform.rotation, rot0, speedRot * Time.deltaTime);
        }        
        else if (typeCam == 1 && (int)transform.eulerAngles.y != (int)rot1.y)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rot1, speedRot * Time.deltaTime);
        }        
        else if (typeCam == 2 && (int)transform.eulerAngles.y != (int)rot2.y)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rot2, speedRot * Time.deltaTime);
        }

        distance = Vector3.Distance(cam.position, playerPos.position);
    }
}
