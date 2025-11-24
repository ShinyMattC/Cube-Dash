using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorManager : MonoBehaviour
{
    public ItemController[] itemButtons;
    public GameObject[] itemPrefabs;
    public GameObject[] itemImage;
    public int curButtonPressed;
    

    public bool isInEditor = true;
    

    void Start()
    {
        
    }
    private void Update()
    {
        Vector3 screenPosition = Input.mousePosition;
        screenPosition.z = 10;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        if(Input.GetMouseButtonDown(0) && itemButtons[curButtonPressed].clicked)
        {
            itemButtons[curButtonPressed].clicked = false;
            GameObject inst = Instantiate(itemPrefabs[curButtonPressed], new Vector3(Mathf.Round(worldPosition.x), Mathf.Round(worldPosition.y), 10), Quaternion.identity);

            LevelSave levelSave = GetComponent<LevelSave>();

            levelSave.OnBlockPlaced(inst.GetComponent<BlockItem>());
            
            Destroy(GameObject.FindGameObjectWithTag("ItemImage"));
        }
    }
    
    
}
