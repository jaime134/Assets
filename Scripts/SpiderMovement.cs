using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    public GameEnding gameEnding;
    float speed = 1;

    void Update()
    {   
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player") gameEnding.CaughtPlayer();

        else if (other.gameObject.tag == "Wall") 
        {
            transform.Rotate(Vector3.up * 90 * Random.Range(0,3));
            Debug.Log("Toca el wall");
        }
    }
}
