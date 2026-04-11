using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class JumpOrb : MonoBehaviour
{
    GameObject player;

    Rigidbody playerRb;

    MovePlayer playerMove;
    private void Awake() {

    }
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            playerRb = player.GetComponent<Rigidbody>();
            playerMove = player.GetComponent<MovePlayer>();
            
        }
    }*/
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
            playerRb.AddForce((playerMove.isUpsideDown ? Vector3.down : Vector3.up) * jumpForce, ForceMode.Impulse);
        }
        
    }
    public void GetPlayer(Component sender, params object[] data)
    {
        if((string)data[3] == this.tag)
        {
            player = sender.gameObject;
        playerRb = sender.GetComponent<Rigidbody>();
        playerMove = (MovePlayer)data[0];
        JumpOrbFunction((int)data[2]);
        }
        
    }
    
    private void Start()
    {

    }
    
    private void Update() {

    }
}
