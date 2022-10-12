using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    public GameEnding gameEnding;
    public GameObject nextWaypoint;
    float speed = 3f;
    float turnSpeed = 100f;
    GameObject lastWaypoint = null;
    Quaternion _lookRotation;
    Vector3 _direction;

    void Update()
    {   
        if (transform.position == nextWaypoint.transform.position)
            {
                var listNeighbors = nextWaypoint.gameObject.GetComponent<Neighbors>().neighbors;
                var newWaypoint = listNeighbors[Random.Range(0, listNeighbors.Length)];

                while (newWaypoint == lastWaypoint)
                {
                    newWaypoint = listNeighbors[Random.Range(0, listNeighbors.Length)];
                }

                lastWaypoint = nextWaypoint;
                nextWaypoint = newWaypoint;

                _direction = (nextWaypoint.transform.position - transform.position).normalized;
                _lookRotation = Quaternion.LookRotation(_direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * turnSpeed);
            }

            transform.position = Vector3.MoveTowards(transform.position, nextWaypoint.transform.position, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") gameEnding.CaughtPlayer();
    }
}
