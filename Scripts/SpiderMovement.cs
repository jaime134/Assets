using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    public GameEnding gameEnding;
    public GameObject nextWaypoint;
    public float speed = 3f;
    float turnSpeed = 100f;
    GameObject lastWaypoint = null;
    Quaternion _lookRotation;
    Vector3 _direction;
    bool faster = false;

    void Start()
    {
        InvokeRepeating("SpeedUp", 0f, 5f);
    }

    void Update()
    {
        if (!faster) speed = 1f;
        else speed = 3f;
        
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

    void SpeedUp()
    {
        if (faster) faster = false;
        else faster = true;
    }
}
