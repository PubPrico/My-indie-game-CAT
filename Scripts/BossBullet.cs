using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    GameObject player;
    Rigidbody2D rb;
    public int bulletDamage;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0f, -1f).normalized * speed;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<PlayerStats>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
        if(collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }

    public void BulletDeflected()
    {
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(-direction.x, -direction.y).normalized * speed;

        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, -rotation);

        gameObject.tag = "Deflected Bullet";
        
    }
}
