using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class YellowPortal : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        rb = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Rigidbody>();
    }

    public void ReverseGravity()
    {
        Physics.gravity = new Vector3(0, 9.81f, 0);
        
    }
    
}
