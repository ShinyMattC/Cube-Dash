using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Block
{
    public string blockName;
    public int[] position = new int[3];
    public int[] rotation = new int[3];
    public int groupID;
    public int instanceID;
    public void DisplayBlockDetails(Block b)
    {
        Debug.Log("name: " + b.blockName + " pos: ");
        for(int j = 0; j < b.position.Length; j++)
        {
            Debug.Log($"{b.position[j]}");
        }
    }
}