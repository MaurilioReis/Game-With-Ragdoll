using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMob : MonoBehaviour
{
    public GameObject mobSpawn;
    public Quaternion rotMob;
    public int limitSpawn = 50;

    public List<GameObject> mobs = new List<GameObject>();

    public Transform areaSpawn;

    public float timeSpawn = 3;

    void Start()
    {
        StartCoroutine("LoopSpawn");
    }

    IEnumerator LoopSpawn ()
    {
        float x = Random.Range(areaSpawn.position.x - (areaSpawn.localScale.y / 2) , areaSpawn.position.x + (areaSpawn.localScale.y / 2));
        float z = Random.Range(areaSpawn.position.z - (areaSpawn.localScale.x / 2) , areaSpawn.position.z + (areaSpawn.localScale.x / 2));
        Vector3 newPos = new Vector3(x, 0.3f, z);
        yield return new WaitForSeconds(timeSpawn);

        if (mobs == null || mobs.Count < limitSpawn)
        {
            GameObject inst = Instantiate(mobSpawn, newPos, rotMob);
            mobs.Add(inst);
        }

        StartCoroutine("LoopSpawn");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Mob")
        {
            mobs.Remove(other.gameObject);
        }
    }
}
