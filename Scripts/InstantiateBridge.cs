using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBridge : MonoBehaviour
{
    public GameObject bridge;
    float timer; public float spawnTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Bridge"))
        {
            return;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.CompareTag("Bridge"))
        {
            StartCoroutine(SpawnBridge());
        }
    }

    IEnumerator SpawnBridge()
    {
        yield return new WaitForSeconds(spawnTime);

        Instantiate(bridge, transform.position, Quaternion.identity);
    }
}
