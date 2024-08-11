using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    AudioSource audioSource;
    Rigidbody2D rb;
    Transform player;
    public GameObject infoCanva;
    public Transform liftSwitch;
    public Transform topPos, botPos;
    public bool isLiftUp;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
        infoCanva.SetActive(false);
        //transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(player.position, liftSwitch.position) > 2f)
        {
            infoCanva.SetActive(false);
        }
        StartLift();

        if(transform.position.y == topPos.position.y && isLiftUp == true)
        {
            audioSource.Stop();
        }
        else if(transform.position.y == botPos.position.y && isLiftUp == false)
        {
            audioSource.Stop();
        }
    }
    void StartLift()
    {
        if(Vector2.Distance(player.position, liftSwitch.position) < 2f && Input.GetKeyDown(KeyCode.E))
        {
            audioSource.Play();
            
            infoCanva.SetActive(false);
            if(transform.position.y >= topPos.position.y)
            {
                isLiftUp = true;
            }
            else if(transform.position.y <= botPos.position.y)
            {
                isLiftUp = false;
            }
            
            
        }

        if(isLiftUp)
        {
            transform.position = Vector2.MoveTowards(transform.position, botPos.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, topPos.position, speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            infoCanva.SetActive(true);
        }
    }

    void OnCollisionExit2D(Collision2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            infoCanva.SetActive(false);
        }
    }

    
}
