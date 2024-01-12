using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStatsInGame : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public float defaultSpeed, defaultRotationSpeed;


    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        ResetSpeed();
    }

    public void ResetSpeed()
    {
        playerMovement.speed = defaultSpeed;
        playerMovement.rotationSpeed = defaultRotationSpeed;
    }
}
