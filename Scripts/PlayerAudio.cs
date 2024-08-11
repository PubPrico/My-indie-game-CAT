using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    PlayerStats playerStats;
    PlayerMovement playerMovement;
    AudioSource audioSource;
    public AudioClip[] audioClips;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && playerMovement.IsGrounded())
        {
            audioSource.PlayOneShot(audioClips[0]);
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && playerMovement.canDash)
        {
            audioSource.PlayOneShot(audioClips[1]);
        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Enemy") || collider.CompareTag("Bullet"))
        {
            audioSource.PlayOneShot(audioClips[2]);
        }
    }
}
