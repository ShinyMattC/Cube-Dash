using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class YellowPortal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    /*private void Update()
    {
        rb = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Rigidbody>();
    }*/

    public void ReverseGravity(Component sender, params object[] data)
    {
        if((string)data[3] == this.tag)
        {
            sender.GetComponent<MovePlayer>().isUpsideDown = true;
            Physics.gravity = new Vector3(0, 9.81f, 0);
        }
        
    }
    
}
