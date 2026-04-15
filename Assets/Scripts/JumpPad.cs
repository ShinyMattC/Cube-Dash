using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    GameObject player;

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
        if(Input.GetMouseButton(0))
        {
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.GetComponent<Rigidbody>().AddForce((player.GetComponent<MovePlayer>().isUpsideDown ? Vector3.down : Vector3.up) * jumpForce, ForceMode.Impulse);
        }
        
    }
    public void GetPlayer(Component sender, params object[] data)
    {
        if((string)data[3] == this.tag)
        {
            player = sender.gameObject;
        JumpPadFunction((int)data[2]);
        }
        
    }
}
