using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaquinaEstados : MonoBehaviour
{

    public static bool pursue;
    public static  bool patrol;
    public static bool called;

    void Start()
    {
        patrol = true;
        pursue = false;
        called = false;
    }


    void Update()
    {
        /*if(GargoyleObserver.detected){

            patrol = false;
            called = true;

        }

        if(Range.detected){

            patrol = false;
            called = false;
            pursue = true;
        }

        if(called== false && pursue ==false){

            patrol = true;
        }*/

    }



}
