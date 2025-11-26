using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class JumpOrb : MonoBehaviour
{
    GameObject player;

    Rigidbody playerRb;

    MovePlayer playerMove;
    private void Awake() {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            playerRb = player.GetComponent<Rigidbody>();
            playerMove = player.GetComponent<MovePlayer>();
            
        }
    }
    private void Start()
    {

    }
    
    private void Update() {

    }
}
