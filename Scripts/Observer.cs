﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public static bool m_IsPlayerInRange;

    void OnTriggerEnter (Collider other)
    {

        if (other.transform == player)
        {
            /*Ghost0Movement.pursue = true;
            Ghost1Movement.pursue = true;
            Ghost2Movement.pursue = true;
            Ghost3Movement.pursue = true;*/

            m_IsPlayerInRange = true;

        }

        
    }





    /*void OnTriggerExit (Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }*/

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
                    Ghost0Movement.pursue = true;
                    Ghost1Movement.pursue = true;
                    Ghost2Movement.pursue = true;
                    Ghost3Movement.pursue = true;
                }
            }
        }
    }
}
