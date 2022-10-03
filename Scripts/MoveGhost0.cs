using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGhost0 : MonoBehaviour
{

    public static bool called;

    public static Vector3 calledPosition;

    public Transform gargola;

    public float velocidad;


    // Start is called before the first frame update
    void Start()
    {
        called = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(called){


            transform.position = Vector3.MoveTowards(transform.position, new Vector3(gargola.position.x,gargola.position.y,gargola.position.z),velocidad);

           // called = false;

        }

        



        
    }
}
