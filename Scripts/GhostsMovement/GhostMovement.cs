using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour
{
   Quaternion _lookRotation;
    Vector3 _direction;
    
    float m_velocidad = 1.5f;
    float turnSpeed = 50f;

    public GameObject nextWaypoint;
    public static GameObject calledWaypoint;
    public GameObject pursueWaypoint;

    GameObject lastWaypoint = null;


    public static bool pursue;
    public static bool patrol;
    public static bool called;


    void Start()
    {
        patrol = true;
        called = false;
        pursue = false;

    }

    void FixedUpdate()
    {
        if(patrol)
        {

            Debug.Log("PATRUYANDO");

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


        if (called)
        {
            patrol = false;
            pursue = false;

            if (transform.position == calledWaypoint.transform.position)
            {

                //MaquinaEstados.called = false;
                //GargoyleObserver.detected = false;
                called = false;
                patrol = true;
                
            }

            Debug.Log("CALLED");

            if (transform.position == nextWaypoint.transform.position)
            {
                var listNeighbors = nextWaypoint.gameObject.GetComponent<Neighbors>().neighbors;

                float minDist = 100000000f;
                GameObject newWaypoint = null;

                foreach (GameObject neighbor in listNeighbors)
                {
                    float dist = Mathf.Sqrt(Mathf.Pow(neighbor.transform.position.x - calledWaypoint.transform.position.x, 2) + Mathf.Pow(neighbor.transform.position.z - calledWaypoint.transform.position.z, 2));
                    if ((dist < minDist) && (neighbor != lastWaypoint))
                    {
                         minDist = dist;
                         newWaypoint = neighbor;
                    }
                    
                }

                lastWaypoint = nextWaypoint;
                nextWaypoint = newWaypoint;

                _direction = (nextWaypoint.transform.position - transform.position).normalized;
                _lookRotation = Quaternion.LookRotation(_direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * turnSpeed);
            }

            transform.position = Vector3.MoveTowards(transform.position, nextWaypoint.transform.position, m_velocidad * Time.deltaTime);
           
        }

        if (pursue)
        {
            patrol = false;
            called = false;

            //Poner aqui las condiciones de parada (chocar al jugador o que se acabe el tiempo de persecucui�n)

            Debug.Log("PERSIGUIENDO");

            if (transform.position == nextWaypoint.transform.position)
            {
                var listNeighbors = nextWaypoint.gameObject.GetComponent<Neighbors>().neighbors;

                float minDist = 100000000f;
                GameObject newWaypoint = null;
    

                foreach (GameObject neighbor in listNeighbors)
                {

                
                    float dist = Mathf.Sqrt(Mathf.Pow(neighbor.transform.position.x - pursueWaypoint.transform.position.x, 2) + Mathf.Pow(neighbor.transform.position.z - pursueWaypoint.transform.position.z, 2));
                    
                    if (dist < minDist)// && (neighbor != lastWaypoint))
                    {
                        minDist = dist;
                        newWaypoint = neighbor;
                
                    }
                    
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
