using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCheck : MonoBehaviour
{
    public GameObject player;
    Spike spike;

    public GameObject spawnPoint;

    public CamFollow camFollow;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Spawn Point");
        camFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Block") {
            player.transform.position = spawnPoint.transform.position;
            Physics.gravity = new Vector3(0, -9.81f, 0); 
            player.GetComponent<MovePlayer>().isUpsideDown = false;
            player.GetComponent<MovePlayer>().speed = 5f;
            player.GetComponent<MovePlayer>().gameMode = gamemode.Cube;
            camFollow.transform.position = camFollow.followerOriginalPosition;
            camFollow.offset = camFollow.offsetOriginal;
        }
    }
}
