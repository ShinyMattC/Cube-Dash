using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EditorCamMove : MonoBehaviour
{
    public int speed;
    public int mouseScrollSpeed = 3;
    void Update()
    {
        #if UNITY_ANDROID
        foreach(Touch t in Input.touches)
        {
            if(t.phase == TouchPhase.Moved)
            {
                Vector3 touchinput = t.deltaPosition;
                Vector3 mobile_dir = touchinput.normalized;

                Vector3 mobile_velocity = touchinput;
                Vector3 mobile_moveAmount = mobile_velocity * Time.unscaledDeltaTime;

                transform.position += mobile_moveAmount;

                Camera.main.orthographicSize += Input.GetAxis("Mouse ScrollWheel");
            }
        }
        
        #endif
        
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        Vector3 dir = input.normalized;

        Vector3 velocity = dir * speed;
        Vector3 moveAmount = velocity * Time.unscaledDeltaTime;

        transform.position += moveAmount;

        Camera.main.orthographicSize += Input.GetAxis("Mouse ScrollWheel");
    }
}
