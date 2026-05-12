using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    GameObject player;
    Rigidbody playerRb;
    MovePlayer playerMove;

    private void Start()
    {

    }
    public void JumpPadFunction(float jumpForce)
    {
        /*if(playerMove.isUpsideDown == false)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else
        {
            playerRb.AddForce(Vector3.down * jumpForce, ForceMode.Impulse);
        }*/
            playerRb.velocity = Vector3.zero;
            playerRb.AddForce((playerMove.isUpsideDown ? Vector3.down : Vector3.up) * jumpForce , ForceMode.Impulse);
        
    }
    public void GetPlayer(Component sender, params object[] data)
    {
        if((string)data[3] == this.tag)
        {
            player = sender.gameObject;
        playerRb = sender.GetComponent<Rigidbody>();
        playerMove = (MovePlayer)data[0];
        JumpPadFunction((int)data[2] - 2);
        }
        
    }
}
