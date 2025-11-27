using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Triggerrole {Top, Bottom}
public class CameraYTriggers : MonoBehaviour
{
    public Triggerrole triggerRole;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            CamFollow camFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamFollow>();
            if (camFollow = null)
            {
                Debug.LogWarning("No CamFollow script found. Are there cameras in this scene?");
            }
            GameObject player = GameObject.Find("Player");
            camFollow.offset.y = player.transform.position.y;
        }
    }
}
