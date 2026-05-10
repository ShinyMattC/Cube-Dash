using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class BlockItem : MonoBehaviour
{
    public int id;
    public Vector3 curPosition;
    public Vector3 curRotation;
    public LevelEditorManager editor;
    public string _name;

    private bool isSelected = false;

    public Color objectOriginalColor;

    // Start is called before the first frame update
    void Start()
    {
        objectOriginalColor = this.gameObject.GetComponent<Renderer>().material.color;
    }
    private void Update()
    {
        curPosition = this.gameObject.transform.position;
        curRotation = this.gameObject.transform.rotation.eulerAngles;
    }

    void OnMouseOver() {
        editor = GameObject.FindGameObjectWithTag("Level Editor Manager").GetComponent<LevelEditorManager>();
        if( editor!= null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                Renderer renderer = this.gameObject.GetComponent<Renderer>();
                
                isSelected = (isSelected == true) ? false : true;
                renderer.material.SetColor("_Color", (isSelected == true) ? Color.green : objectOriginalColor);
                
                
            }
            if (isSelected)
            {
               
                if (Input.GetMouseButtonDown(1))
                {
                    isSelected = false;
                    LevelSave levelSave = editor.GetComponent<LevelSave>();
    
                    levelSave.OnBlockRemoved(this.GetComponent<BlockItem>());
                    Destroy(this.gameObject);
                    editor.itemButtons[id].quantity++;
                    editor.itemButtons[id].quantityText.text = editor.itemButtons[id].quantity.ToString();
                }
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    this.GetComponent<Transform>().Rotate(0, 0, 45);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    this.GetComponent<Transform>().Rotate(0, 0, -45);
                }
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    this.GetComponent<Transform>().Rotate(0, -180, 0);
                }
                if (Input.GetKeyDown(KeyCode.X))
                {
                    this.GetComponent<Transform>().Rotate(0, 180, 0);
                }
                if (Input.GetKeyDown(KeyCode.J))
                {
                    this.GetComponent<Transform>().Translate(-0.5f, 0, 0, Space.World);
                }
                if (Input.GetKeyDown(KeyCode.L))
                {
                    this.GetComponent<Transform>().Translate(0.5f, 0, 0, Space.World);
                }
                if (Input.GetKeyDown(KeyCode.I))
                {
                    this.GetComponent<Transform>().Translate(0, 0.5f, 0, Space.World);
                }
                if (Input.GetKeyDown(KeyCode.K))
                {
                    this.GetComponent<Transform>().Translate(0, -0.5f, 0, Space.World);
                }
            }
            
            
        }
        else {
            return;
        }
        
    }
    public void ButtonClick(Component sender, params object[] data)
    {
        if(isSelected)
        {
          TransformButton t = (TransformButton)sender;
        switch((editorbuttontype)data[0])
        {
            case editorbuttontype.MoveLeft:
            this.GetComponent<Transform>().Translate(-0.5f, 0, 0, Space.World);
            break;
            case editorbuttontype.MoveRight:
            this.GetComponent<Transform>().Translate(0.5f, 0, 0, Space.World);
            break;
            case editorbuttontype.MoveUp:
            this.GetComponent<Transform>().Translate(0, 0.5f, 0, Space.World);
            break;
            case editorbuttontype.MoveDown:
            this.GetComponent<Transform>().Translate(0, -0.5f, 0, Space.World);
            break;
            case editorbuttontype.RotateRight45:
            this.GetComponent<Transform>().Rotate(0, 0, 45);
            break;
            case editorbuttontype.RotateLeft45:
            this.GetComponent<Transform>().Rotate(0, 0, -45);
            break;
            case editorbuttontype.FlipH:
            this.GetComponent<Transform>().Rotate(0, -180, 0);
            break;
            case editorbuttontype.FlipV:
            this.GetComponent<Transform>().Rotate(0, 180, 0);
            break;
            case editorbuttontype.Delete:
                isSelected = false;
                LevelSave levelSave = editor.GetComponent<LevelSave>();
    
                levelSave.OnBlockRemoved(this.GetComponent<BlockItem>());
                Destroy(this.gameObject);
                editor.itemButtons[id].quantity++;
                editor.itemButtons[id].quantityText.text = editor.itemButtons[id].quantity.ToString();
            break;
        }  
        }
        
    }
}

