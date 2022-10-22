using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchPlayer : MonoBehaviour
{
    List<GameObject> nearWaypoints = new List<GameObject>();
    public GameObject firstWaypoint;
    public static GameObject myWaypoint;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Waypoint")
        {
            nearWaypoints.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Waypoint")
        {
            nearWaypoints.Remove(other.gameObject);
        }
    }


    private void Start()
    {
        myWaypoint = firstWaypoint;
    }

    void Update()
    {
        foreach (GameObject wp in nearWaypoints)
        {
            float dist = Mathf.Sqrt(Mathf.Pow(myWaypoint.transform.position.x - transform.position.x, 2) + Mathf.Pow(myWaypoint.transform.position.z - transform.position.z, 2));
            float newDist = Mathf.Sqrt(Mathf.Pow(wp.transform.position.x - transform.position.x, 2) + Mathf.Pow(wp.transform.position.z - transform.position.z, 2));
            if (newDist < dist)
            {
                myWaypoint = wp;
            }
        }
    }
}
