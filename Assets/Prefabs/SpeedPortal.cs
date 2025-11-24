using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPortal : MonoBehaviour
{
    public enum SpeedValues
    {
        VerySlow = 2,
        Slow = 3,
        Normal = 5,
        Fast = 7,
        VeryFast = 9
    };
    public SpeedValues speedValues;
    MovePlayer player;

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MovePlayer>();
    }
    public void ChangeSpeed(float speed)
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<MovePlayer>(); 
        player.speed = speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player = other.gameObject.GetComponent<MovePlayer>();
            ChangeSpeed((float)speedValues);

        }
    }
}
