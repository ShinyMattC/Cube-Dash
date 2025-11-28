using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public GameObject player;

    public GameObject spawnPoint;

    public CamFollow camFollow;
    private void Awake() {
    }
    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawnPoint = GameObject.FindGameObjectWithTag("Spawn Point");
    }
    private void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Spawn Point");
        camFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamFollow>();
    }
    public void SetPlayer(GameObject _player)
    {
        player = _player;
    }
    public void Die() 
    { 
        player.transform.position = spawnPoint.transform.position;
        Physics.gravity = new Vector3(0, -9.81f, 0); 
        player.GetComponent<MovePlayer>().isUpsideDown = false;
        player.GetComponent<MovePlayer>().speed = 5f;
        player.GetComponent<MovePlayer>().gameMode = gamemode.Cube;
        camFollow.transform.position = camFollow.followerOriginalPosition;
        camFollow.offset = camFollow.offsetOriginal;
        //player.GetComponent<AudioSource>().Stop();
        //player.GetComponent<AudioSource>().Play();
    } 

}
