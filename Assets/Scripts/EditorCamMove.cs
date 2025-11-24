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
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        Vector3 dir = input.normalized;

        Vector3 velocity = dir * speed;
        Vector3 moveAmount = velocity * Time.deltaTime;

        transform.position += moveAmount;

        Camera.main.orthographicSize += Input.GetAxis("Mouse ScrollWheel");
    }
}
