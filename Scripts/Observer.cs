using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;

    void OnTriggerEnter (Collider other)
    {
            
        if (other.transform == player)
        {
            GhostMovement.pursue = true;
            //Ghost1Movement.pursue = true;
            //Ghost2Movement.pursue = true;
            //Ghost3Movement.pursue = true;

        }
    }





    /*void OnTriggerExit (Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }*/

    /*void Update ()
    {
        if (m_IsPlayerSeen)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;
            
            if (Physics.Raycast (ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    Debug.Log(raycastHit.transform);
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }*/
}
