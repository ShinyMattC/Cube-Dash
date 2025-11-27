using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Triggerrole {Top, Bottom}
public class CameraYTriggers : MonoBehaviour
{
    public Triggerrole triggerRole;
    public CamFollow camFollow;
    public GameObject player; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {

            
            camFollow.offset.y = player.transform.position.y;
        }
    }
}
