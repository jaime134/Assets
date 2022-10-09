using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost3Movement : MonoBehaviour
{
    //public NavMeshAgent navMeshAgent;

    Rigidbody m_Rigidbody;
    Quaternion m_Rotation = Quaternion.identity;
    
    float m_velocidad = 1.5f;
    float turnSpeed = 20f;

    public GameObject nextWaypoint;
    GameObject lastWaypoint = null;

    public static bool called;
    public static Vector3 calledPosition;

    void Start()
    {
        called = false;
        m_Rigidbody = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
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
        }


        if (called)
        {
            //Destination: calledPosition
            called = false;
        }

        else
        {
            //Vector3 desiredForward = Vector3.RotateTowards(transform.forward, nextWaypoint.transform.position, turnSpeed * Time.deltaTime, 0f);
            //m_Rotation = Quaternion.LookRotation(desiredForward);
            //m_Rigidbody.MoveRotation(m_Rotation);
            transform.position = Vector3.MoveTowards(transform.position, nextWaypoint.transform.position, m_velocidad * Time.deltaTime);
            
        }


    }
}
