using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBlockList : MonoBehaviour
{
    public LevelEditorManager lm;
    public Dictionary<int, Vector3> blocks;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GetBlocks());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Dictionary<int, Vector3> GetBlocks()
    {
        return blocks;
    }
    public void AddBlocks(int blockID, Vector3 blockPos)
    {
        try
        {
            // blocks.Add(block.id, block.curPosition);
            blocks.Add(blockID, blockPos);
            Debug.Log($"Added block to dictionary (maybe). To Check: {blocks.Count} is more than null, so it works.");
        }
        catch (NullReferenceException e)
        {
            Debug.LogWarning($"Could not add block to dictionary: {e.Message}");
        }

    }

}
