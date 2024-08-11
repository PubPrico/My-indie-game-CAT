using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject player;
    public GameObject infoCanva;
    bool nearObj;
    // Start is called before the first frame update
    void Start()
    {
        nearObj = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && nearObj == true)
        {
            if(gameObject.CompareTag("Sword"))
            {
                player.GetComponent<Sword>().enabled = true;
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            Debug.Log("Detected");
            nearObj = true;
            infoCanva.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            nearObj = false;
            infoCanva.SetActive(false);
        }
    }
}
