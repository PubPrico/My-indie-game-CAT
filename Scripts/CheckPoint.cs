using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    GameMaster gm;
    public PlayerStats playerStats;
    GameObject[] spawnPoint;
    float timer = 0f;
    bool isHealing;
    int currentHp, maxHp;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("Game Master").GetComponent<GameMaster>();
        isHealing = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerStats.healthBar.SetBarValue(playerStats.currentHp);
        if(isHealing == true && playerStats.currentHp < playerStats.maxHp)
        {
            timer += Time.deltaTime;
            if(timer >= 0.3f)
            {
                Debug.Log("Triggered");
                playerStats.currentHp++;
                timer = 0f;
            }
            
        }
        else if(currentHp >= maxHp)
        {
            isHealing = false;
            
        }

        
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            gm.lastCheckPointPos = transform.position;
            isHealing = true;
        
        }
    }
}
