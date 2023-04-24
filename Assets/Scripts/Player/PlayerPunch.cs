using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    public float forcePunch = 1;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Mob")
        {
            MobController scriptMob = collision.gameObject.GetComponent<MobController>();
            scriptMob.ReceiveAtack(transform, forcePunch);

            if (anim.GetFloat("Right") == 1)
            {
                anim.SetFloat("Right", 0);
            }
            else
            {
                anim.SetFloat("Right", 1);
            }

            anim.SetTrigger("Punch");
        }
    }
}
