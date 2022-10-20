using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public Canvas keyCanvas;
    public GameEnding gameEnding;
    public GameObject key;
    public GameObject[] waypoints;
    public bool hasKey;

    void Start()
    {
        keyCanvas.enabled = false;
        hasKey = false;

        key.transform.position = waypoints[Random.Range(0, waypoints.Length)].transform.position;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            hasKey = true;
            keyCanvas.enabled = true;
            key.SetActive(false);
        }
    }
}
