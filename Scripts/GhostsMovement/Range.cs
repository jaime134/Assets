using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    public static bool detected;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        detected = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        void OnTriggerEnter (Collider other)
    {
        //var gargoyle = GameObject.FindWithTag("Gargoyle");
            
        if (other.transform == player)
        {
            detected = true;
        }
    }

  
    void OnTriggerExit (Collider other)
    {
        if (other.transform == player)
        {

            detected = false;

        }
    }

  
}
