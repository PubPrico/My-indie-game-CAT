using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    public GameObject blinkEffect;

    void Start()
    {
        blinkEffect.SetActive(false);
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<EnemyHeadCheck>())
        {
            StartCoroutine(Blink());
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
        Destroy(transform.parent.gameObject);
        
    }
}
