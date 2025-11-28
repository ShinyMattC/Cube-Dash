using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamFollow : MonoBehaviour
{
    public Transform player;

    public Vector3 offset;

    public float speed;

    public GameObject yAxisTriggerTop, yAxisTriggerBottom;

    public GameObject follower;

    public Vector3 followerOriginalPosition;

    private void Start()
    {
        followerOriginalPosition = follower.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //.position = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, player.transform.position.z - offset.z);
        transform.position = new Vector3(player.transform.position.x + offset.x, offset.y, player.transform.position.z + offset.z);
    }
}
