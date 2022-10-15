using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost3Movement : MonoBehaviour
{
    //public NavMeshAgent navMeshAgent;

    Rigidbody m_Rigidbody;
    Quaternion m_Rotation = Quaternion.identity;
    Quaternion _lookRotation;
    Vector3 _direction;
    
    float m_velocidad = 1.5f;
    float turnSpeed = 50f;

    public GameObject nextWaypoint;
    public GameObject calledWaypoint;
    GameObject lastWaypoint = null;

    public static bool called;
    public static Vector3 calledPosition;

    void Start()
    {
        called = true;  //Cambiar a false
        m_Rigidbody = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {

        if (called)
        {
            //Destination: calledPosition
            if (transform.position == nextWaypoint.transform.position)
            {
                var listNeighbors = nextWaypoint.gameObject.GetComponent<Neighbors>().neighbors;

                float minDist = 100000000f;
                GameObject newWaypoint = null;
                Debug.Log("prueba1");

                foreach (GameObject neighbor in listNeighbors)
                {
                    Debug.Log("prueba2");
                    float dist = Mathf.Sqrt(Mathf.Pow(neighbor.transform.position.x - calledWaypoint.transform.position.x, 2) + Mathf.Pow(neighbor.transform.position.z - calledWaypoint.transform.position.z, 2));
                    if (dist < minDist)
                    {
                        minDist = dist;
                        newWaypoint = neighbor;
                        Debug.Log("prueba3");
                    }
                }

                lastWaypoint = nextWaypoint;
                nextWaypoint = newWaypoint;

                _direction = (nextWaypoint.transform.position - transform.position).normalized;
                _lookRotation = Quaternion.LookRotation(_direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * turnSpeed);
            }

            transform.position = Vector3.MoveTowards(transform.position, nextWaypoint.transform.position, m_velocidad * Time.deltaTime);


            //called = false;
        }

        else
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

            transform.position = Vector3.MoveTowards(transform.position, nextWaypoint.transform.position, m_velocidad * Time.deltaTime);
        }


    }
}
