using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadCheck : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<EnemyHead>())
        {
            audioSource.PlayOneShot(clip);
            gameObject.GetComponent<PlayerMovement>().rb.velocity = new Vector2(0f, 22f);
            
            
            gameObject.GetComponent<PlayerStats>().TakeDamage(0);
        }
    }

    
}
