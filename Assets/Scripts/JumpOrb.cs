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
    public void JumpOrbFunction(float jumpForce)
    {
        /*if(playerMove.isUpsideDown == false)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else
        {
            playerRb.AddForce(Vector3.down * jumpForce, ForceMode.Impulse);
        }*/
        if(Input.GetMouseButton(0))
        {
            playerRb.AddForce(((playerMove.isUpsideDown) ? Vector3.down : Vector3.up) * jumpForce, ForceMode.Impulse);
        }
        
    }
    private void Update() {

    }
}
