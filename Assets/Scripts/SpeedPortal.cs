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
    public GameObject player;

    private void Update()
    {
        
    }
    
    public void SetSpeed(Component sender, params object[] data)
    {
        if((string)data[3] == this.tag)
        {
            player.GetComponent<MovePlayer>().speed = (float)speedValues;
            Debug.Log($"{sender}'s speed changed");
        }
    }
    public void SetPlayer(Component sender, params object[] data)
    {
        player = sender.gameObject;
    }
    /*private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player = other.gameObject.GetComponent<MovePlayer>();
            ChangeSpeed((float)speedValues);

        }
    }*/
}
