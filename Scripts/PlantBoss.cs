using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBoss : MonoBehaviour
{
    AudioSource audioSource; AudioClip clip;
    public Animator anim;
    public GameObject bullet;
    public Transform bulletPos;
    public float shootDistance;
    GameObject player;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        clip = audioSource.clip;
        anim.SetBool("IsShooting", false);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if(distance < shootDistance)
        {
            
            if(timer > 2)
            {
                timer = 0;
                StartCoroutine(Shoot());
            }
            else
            {
                anim.SetBool("IsShooting", false);
            }
        }

        

    }

    IEnumerator Shoot()
    {
        anim.SetBool("IsShooting", true);

        yield return new WaitForSeconds(0.8f);

        audioSource.PlayOneShot(clip);
        Instantiate(bullet, bulletPos);
        
    }

    void OnDrawGizmosSelected()
    {
        if(bulletPos == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(bulletPos.position, shootDistance);
    }
}
