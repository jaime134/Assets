using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostColision : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;

    void OnTriggerEnter(Collider other)
    {

        if (other.transform == player)
        {
            gameEnding.CaughtPlayer();
        }
    }
}
