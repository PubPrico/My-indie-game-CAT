using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public AudioSource audio;
    public ParticleSystem flashEffect, exploEffect;
    public Animator anim;
    public bool isAttacking;
    public Transform atkPoint, parryPoint;
    public float atkRange, parryRange;
    public int playerDamage;
    public LayerMask enemyLayers, bulletLayers;
    bool isParrying;
    float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        isParrying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
        }
        if(Input.GetKey(KeyCode.X) && gameObject.GetComponent<PlayerStats>().currentStamina >= 25)
        {
            Defence();
        }
        
        
        
    }
    void Attack()
    {
        anim.SetTrigger("Attack");
        StartCoroutine(ForAttack());
        Debug.Log("Attack!");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(atkPoint.position, atkRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyStats>().TakeDamage(playerDamage);
            audio.Play();
            flashEffect.Play();
            exploEffect.Play();
        }
    }

    IEnumerator ForAttack()
    {
        isAttacking = true;

        yield return new WaitForSeconds(1f);

        isAttacking = false;
    }

    void Defence()
    {
        Collider2D[] enemyBullets = Physics2D.OverlapCircleAll(atkPoint.position, atkRange, bulletLayers);

        foreach(Collider2D enemyBullet in enemyBullets)
        {
            float distance = Vector2.Distance(parryPoint.transform.position, enemyBullet.transform.position);

            Debug.Log(distance + "DEF");
            if(distance > parryRange)
            {
                anim.SetTrigger("Attack");
                Destroy(enemyBullet.gameObject);
                gameObject.GetComponent<PlayerStats>().TakeStamina(25);
                audio.Play();
            flashEffect.Play();
            exploEffect.Play();

            }
            
        }
    }

    void Parry()
    {
        Collider2D[] enemyBullets= Physics2D.OverlapCircleAll(atkPoint.position, atkRange, bulletLayers);

        foreach(Collider2D enemyBullet in enemyBullets)
        {
            float distance = Vector2.Distance(parryPoint.transform.position, enemyBullet.transform.position);
            if(distance >= parryRange && distance <= atkRange)
            {
                Debug.Log(distance + "PARRY");
                
                enemyBullet.GetComponent<EnemyBullet>().BulletDeflected();
                isParrying = true;
            }
            
            
        }
    }

    IEnumerator IsParryState()
    {
        float parryTime = 0;
        
        while(timer < 1f)
        {
            if(isParrying == false)
            {
                break;
            }
            parryTime += Time.deltaTime;

            yield return null;
        }
        
    }

    
    void OnDrawGizmosSelected()
    {
        if(atkPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(atkPoint.position, atkRange);

        if(parryPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(parryPoint.position, parryRange);
    }
}
