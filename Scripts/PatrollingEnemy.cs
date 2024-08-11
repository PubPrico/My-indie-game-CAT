using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    GameObject player;
    public Transform originalPosition, patrolRange;
    public int speed;
    float timer;
    public float duration; 
    public float wallDistance, groundDistance, playerDistance;
    public bool isMovingRight = true;
    public Transform wallDetection, groundDetection;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if(transform.position.x >= patrolRange.position.x)
        {
            if(isMovingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                isMovingRight = false;
            }
            
        }
        else if(transform.position.x <= originalPosition.position.x)
        {
            if(isMovingRight == false)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isMovingRight = true;
            }
        }
            
        

        if(wallDistance > 0)
        {
            RaycastHit2D wallInfo = Physics2D.Raycast(wallDetection.position, Vector2.right, wallDistance);

            if(wallInfo.collider.CompareTag("Ground"))
            {
                if(isMovingRight == true)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    isMovingRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    isMovingRight = true;
                }
            }
            else
            {
                return;
            }
        }
        
        else if(groundDistance > 0)
        {
            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, groundDistance);

            if(groundInfo.collider == false)
            {
                if(isMovingRight == true)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    isMovingRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    isMovingRight = true;
                }
            }
            else
            {
                return;
            }
        }

        

    }


    

}
