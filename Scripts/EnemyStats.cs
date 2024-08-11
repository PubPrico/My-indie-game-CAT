using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int maxHp, enemyDamage;
    int currentHp;
    GameObject player;
    public GameObject blinkEffect;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentHp = maxHp;
        blinkEffect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        /*if(collider.CompareTag("Player"))
        {
            player.GetComponent<PlayerStats>().TakeDamage(enemyDamage);
        }*/
        if(collider.CompareTag("Deflected Bullet"))
        {
            int bulletDamage = collider.GetComponent<EnemyBullet>().bulletDamage;
            TakeDamage(bulletDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(Blink());
        currentHp -= damage;

        if(currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Blink()
    {
        
        blinkEffect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        blinkEffect.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        blinkEffect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        blinkEffect.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        blinkEffect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        blinkEffect.SetActive(false);
        
    }
}
