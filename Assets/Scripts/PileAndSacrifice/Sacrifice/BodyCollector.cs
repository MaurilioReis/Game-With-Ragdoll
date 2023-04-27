using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCollector : MonoBehaviour
{
    public Animator totemAnim;
    public GameObject impactFX;
    public GameObject dropSoul;
    public Transform posDirectionDropSoul;
    public float forceDrop = 1;
    public SystemLevel systemLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Mob")
        {
            Destroy(other.gameObject);

            totemAnim.SetTrigger("Soul");

            Instantiate(impactFX, transform.position, transform.rotation);

            GameObject soul = Instantiate(dropSoul, posDirectionDropSoul.position, posDirectionDropSoul.rotation);
            soul.GetComponent<Rigidbody>().AddForce(posDirectionDropSoul.forward * forceDrop, ForceMode.Impulse);

            systemLevel.AddXP(450);
        }
    }
}
