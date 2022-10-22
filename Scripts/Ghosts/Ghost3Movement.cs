using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Ghost3Movement : MonoBehaviour
{
    Quaternion _lookRotation;
    Vector3 _direction;

    float m_velocidad = 1.4f;
    float turnSpeed = 50f;

    public GameObject nextWaypoint;
    public static GameObject calledWaypoint;
    public GameObject pursueWaypoint;
    public GameObject player;

    GameObject lastWaypoint = null;

    public static bool pursue;
    public static bool patrol;
    public static bool called;

    public float timer = 0;
    public int patrolTime = 10;

    void Start()
    {
        patrol = true;
        called = false;
        pursue = false;
    }

    void FixedUpdate()
    {
        //////////////////////////////////////////////////////
        /////              PATRULLANDO               /////////
        //////////////////////////////////////////////////////

        if (patrol)
        {
            //Debug.Log("PATRULLANDO");

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

            _direction = (nextWaypoint.transform.position - transform.position).normalized;
            _lookRotation = Quaternion.LookRotation(_direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * turnSpeed);

            transform.position = Vector3.MoveTowards(transform.position, nextWaypoint.transform.position, m_velocidad * Time.deltaTime);
        }



        //////////////////////////////////////////////////////
        /////              ALERTADO                  /////////
        //////////////////////////////////////////////////////

        if (called)
        {
            //Debug.Log("CALLED");

            patrol = false;
            pursue = false;

            if (transform.position == calledWaypoint.transform.position)
            {
                called = false;
                patrol = true;
            }

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


            }

            _direction = (nextWaypoint.transform.position - transform.position).normalized;
            _lookRotation = Quaternion.LookRotation(_direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * turnSpeed);

            transform.position = Vector3.MoveTowards(transform.position, nextWaypoint.transform.position, m_velocidad * Time.deltaTime);
        }



        //////////////////////////////////////////////////////
        /////              PERSIGUIENDO              /////////
        //////////////////////////////////////////////////////

        if (pursue)
        {
            Debug.Log("PERSIGUIENDO");

            patrol = false;
            called = false;
            timer += Time.deltaTime;

            if (timer >= patrolTime) //Pasado tiempo maximo de patruya
            {
                Debug.Log("VUELTA A LA PATRULLA");

                patrol = true;
                pursue = false;
                timer = 0;

                if (nextWaypoint == player)
                {
                    var newWaypoint = CatchPlayer.myWaypoint;
                    lastWaypoint = nextWaypoint;
                    nextWaypoint = newWaypoint;
                }
            }



            if (transform.position == nextWaypoint.transform.position)
            {
                float minDist = 100000000f;
                GameObject newWaypoint = null;

                if (nextWaypoint == CatchPlayer.myWaypoint)  //Para ir directo al personaje
                {
                    newWaypoint = player;
                }

                else
                {
                    var listNeighbors = nextWaypoint.gameObject.GetComponent<Neighbors>().neighbors;

                    foreach (GameObject neighbor in listNeighbors)
                    {
                        float dist = Mathf.Sqrt(Mathf.Pow(neighbor.transform.position.x - pursueWaypoint.transform.position.x, 2) + Mathf.Pow(neighbor.transform.position.z - pursueWaypoint.transform.position.z, 2));

                        if (dist < minDist)
                        {
                            minDist = dist;
                            newWaypoint = neighbor;
                        }
                    }
                }

                lastWaypoint = nextWaypoint;
                nextWaypoint = newWaypoint;

            }

            _direction = (nextWaypoint.transform.position - transform.position).normalized;
            _lookRotation = Quaternion.LookRotation(_direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * turnSpeed);

            transform.position = Vector3.MoveTowards(transform.position, nextWaypoint.transform.position, m_velocidad * Time.deltaTime);
        }
    }
}
