using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GargoyleObserver : MonoBehaviour
{
    public Transform player;
   

    public Transform[] ghosts;

    Transform m_Transform;
    

    public static bool detected;

    
    void Start()
    {
        m_Transform = GetComponent<Transform>();

         detected = false;
    }
    
    
    void OnTriggerEnter (Collider other)
    {
        //var gargoyle = GameObject.FindWithTag("Gargoyle");
            
        if (other.transform == player)
        {
            detected = true;
            //CallGhost();
        }
    }


/*
    void OnTriggerExit (Collider other)
    {
        if (other.transform == player)
        {
            detected = false;
        }
    }

*/
    void Update(){

       
    }
    /*
    void Update ()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;
            
            if (Physics.Raycast (ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    Debug.Log(raycastHit.transform);
                    // gameEnding.CaughtPlayer ();
                    
                }
            }
        }
    }
    */

    void CallGhost()
    {
        float minDist = 10000;
        int calledGhost = -1;

        for(int i = 0; i < ghosts.Length; i++)
        {
            float dist = Mathf.Sqrt(Mathf.Pow(ghosts[i].position.x - m_Transform.position.x, 2) + Mathf.Pow(ghosts[i].position.z - m_Transform.position.z, 2));
            Debug.Log(dist);
            if ( dist < minDist)
            {
                minDist = dist;
                calledGhost = i;
            }
        }

        if (calledGhost == 0)
        {
            MaquinaEstados.called = true;
            GhostMovement.calledPosition = m_Transform.position;
        }


        /*

        else if (calledGhost == 1)
        {
            Ghost1Movement.called = true;
            Ghost1Movement.calledPosition = m_Transform.position;
        }

        else if (calledGhost == 2)
        {
            Ghost2Movement.called = true;
            Ghost2Movement.calledPosition = m_Transform.position;
        }

        else if (calledGhost == 3)
        {
            Ghost3Movement.called = true;
            Ghost3Movement.calledPosition = m_Transform.position;
        }
        */

        else  //Si es -1
        {
            Debug.Log("Ninguno llamado");
        }



        Debug.Log(minDist);
        Debug.Log(calledGhost);
        Debug.Log("End");
    }
}
