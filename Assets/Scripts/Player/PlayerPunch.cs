using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    public float forcePunch = 1;
    public GameObject fxImpact;

    Animator anim;

    ShakeCam shakeCam;

    private void Start()
    {
        shakeCam = GetComponent<ShakeCam>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Punch") && collision.gameObject.tag == "Mob")
        {
            MobController scriptMob = collision.gameObject.GetComponent<MobController>();
            scriptMob.ReceiveAtack(transform, forcePunch);


            Vector3 offsetY = new Vector3(0, 1.5f, 0);
            GameObject instFX = Instantiate(fxImpact, collision.transform.position + offsetY, collision.transform.rotation);
            instFX.transform.SetParent(collision.transform);

            shakeCam.Shake();

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
