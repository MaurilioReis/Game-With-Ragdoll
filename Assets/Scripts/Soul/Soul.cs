using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Soul : MonoBehaviour
{
    public float time;
    Transform player;
    CapsuleCollider col;
    bool collected;

    public float value;
    public GameObject valueGO;
    GameObject instPrice;

    Transform targetCam;

    private void Start()
    {
        StartCoroutine("EnableCol");

        value = Random.Range(100, 1000);
        transform.localScale = new Vector3(value / 1000, value / 1000, value / 1000);

        instPrice = Instantiate(valueGO, transform.position, transform.rotation);
        TMP_Text txt = instPrice.transform.GetChild(0).GetComponent<TMP_Text>();
        txt.text = ""+value;

        player = GameObject.FindWithTag("Player").transform;

        col = gameObject.GetComponent<CapsuleCollider>();

        targetCam = Camera.main.transform;
    }

    private void Update()
    {
        Vector3 newPosTxt = transform.position;
        newPosTxt.y = newPosTxt.y + 1;
        instPrice.transform.position = newPosTxt;

        instPrice.transform.LookAt(targetCam);

        if (collected == true)
        {
            Vector3 newPosition = player.position;
            newPosition.y = newPosition.y + 2;
            transform.position = Vector3.Lerp(transform.position, newPosition, 4f * Time.deltaTime);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            collected = true;
            GetComponent<Rigidbody>().useGravity = false;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            Destroy(instPrice);
        }
    }

    IEnumerator EnableCol()
    {
        yield return new WaitForSeconds(time);
        col.enabled = true;
    }
}
