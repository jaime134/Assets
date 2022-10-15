using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;

    bool m_IsPlayerInRange;
    AudioSource siren;

    void Start()
    {
        siren = GetComponent<AudioSource>();
    }

    void OnTriggerEnter (Collider other)
    {
        //var gargoyle = GameObject.FindWithTag("Gargoyle");
            
        if (other.transform == player)
        {
            //if (other == gargoyle.GetComponent<Collider>())
            //{
            //    Debug.Log("Is gargoyle");
            //}
            m_IsPlayerInRange = true;
            siren.Play();
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
            siren.Stop();
        }
    }

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
                    //gameEnding.CaughtPlayer();
                }
            }
        }
    }
}
