using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }
    public void JumpPadFunction(float jumpForce)
    {
        player.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);     
    }
}
