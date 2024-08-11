using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public int maxHp, maxStamina;
    public int currentHp, currentStamina;
    public int damageTaken;
    public SliderBar healthBar, StaminaBar;
    public GameObject blinkEffect;
    float timer = 0f;
    public ParticleSystem flashEffect, exploEffect;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        healthBar.SetMaxBarValue(maxHp);
        blinkEffect.SetActive(false);

        currentStamina = maxStamina;
        StaminaBar.SetMaxBarValue(maxStamina);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 0.3f)
        {
            if(currentStamina != maxStamina)
            {
                currentStamina++;
            }
            timer = 0f;
        }

        StaminaBar.SetBarValue(currentStamina);
        
    }

    public void TakeDamage(int enemyDamage)
    {
        StartCoroutine(Invulnerability());

        currentHp -= enemyDamage;
        healthBar.SetBarValue(currentHp);

        if(enemyDamage > 0)
        {
            CamShake.Instance.ShakeCamera(5f, 0.2f);
            StartCoroutine(Blink());
        }
    }

    public void TakeStamina(int staminaDamage)
    {
        currentStamina -= 25;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Enemy"))
        {
            
            playerMovement.rb.velocity = new Vector2(0f, 22f);
            Instantiate(flashEffect, collider.transform.position, Quaternion.identity);
            Instantiate(exploEffect, collider.transform.position, Quaternion.identity);
            
            TakeDamage(damageTaken);
        }
        if(collider.gameObject.CompareTag("Bullet"))
        {
            
            playerMovement.rb.velocity = new Vector2(0f, 22f);
            Instantiate(flashEffect, collider.transform.position, Quaternion.identity);
            Instantiate(exploEffect, collider.transform.position, Quaternion.identity);
        }
        
    }

    IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);

        yield return new WaitForSeconds(1f);

        Physics2D.IgnoreLayerCollision(7, 8, false);
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
