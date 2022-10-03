using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public Canvas keyCanvas;
    public GameEnding gameEnding;
    public GameObject key;
    public bool hasKey;

    void Start()
    {
        keyCanvas.enabled = false;
        hasKey = false;
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
