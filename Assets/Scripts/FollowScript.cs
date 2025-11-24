using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPosition = Input.mousePosition;
        screenPosition.z = 10;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        transform.position = worldPosition;
        
    }
}
