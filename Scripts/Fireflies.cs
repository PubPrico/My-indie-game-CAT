using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireflies : MonoBehaviour
{
    public GameObject[] points;
    Rigidbody2D rb;
    public int speed;
    bool arrived = false;
    Vector3 direction;
    int i;
    GameObject currentPoint;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        i = Random.Range(0, 21);
        currentPoint = points[i];
        direction = currentPoint.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if(currentPoint.transform.position == transform.position)
        {
            arrived = true;
            i = Random.Range(0, 21);
            currentPoint = points[i];
            direction = currentPoint.transform.position - transform.position;
        }
        else
        {
            arrived = false;
        }

        if(arrived == false)
        {
            rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        }
        
    }
}
