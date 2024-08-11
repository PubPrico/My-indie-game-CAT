using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappler : MonoBehaviour
{
    public Camera cam;
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint;
    // Start is called before the first frame update
    void Start()
    {
        distanceJoint.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            Vector2 mousePos = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
            lineRenderer.SetPosition(0, mousePos);
            lineRenderer.SetPosition(1, transform.position);
            distanceJoint.connectedAnchor = mousePos;
            distanceJoint.enabled = true;
            lineRenderer.enabled = true;
        }
        else if(Input.GetKeyUp(KeyCode.Mouse2))
        {
            distanceJoint.enabled = false;
            lineRenderer.enabled = false;
        }

        if(distanceJoint.enabled)
        {
            lineRenderer.SetPosition(1, transform.position);
        }
    }
}
