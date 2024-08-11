using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour
{
    GameMaster gm;
    PlayerStats playerStats;
    public Animator deadPanel;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("Game Master").GetComponent<GameMaster>();
        transform.position = gm.lastCheckPointPos;

        playerStats = gameObject.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerStats.currentHp <= 0)
        {
            StartCoroutine(RevivePlayer());
        }
    }

    IEnumerator RevivePlayer()
    {
        deadPanel.SetTrigger("DeadPanel");

        yield return new WaitForSeconds(0.5f);

        deadPanel.ResetTrigger("DeadPanel");
        transform.position = gm.lastCheckPointPos;
        playerStats.currentHp = playerStats.maxHp;
    }

    IEnumerator SpawnPlayer()
    {
        deadPanel.SetTrigger("DeadPanel");

        yield return new WaitForSeconds(0.5f);

        deadPanel.ResetTrigger("DeadPanel");
        transform.position = gm.lastPos;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Void"))
        {
            playerStats.currentHp -= 5;

            if(playerStats.currentHp > 0)
            {
                StartCoroutine(SpawnPlayer());
            }
            
        }
    }
}
