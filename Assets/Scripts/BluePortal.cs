using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePortal : MonoBehaviour
{

    public void RevertGravity(Component sender, params object[] data)
    {
        if((string)data[3] == this.tag)
        {
            sender.GetComponent<MovePlayer>().isUpsideDown = false;
            Physics.gravity = new Vector3(0, -9.81f, 0);
        }
        

    }
    /*private void OnTriggerEnter(Collider other)
    {
        RevertGravity();
    }*/
}
