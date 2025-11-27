using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BlockItem : MonoBehaviour
{
    public int id;
    public Vector3 curPosition;
    public Vector3 curRotation;
    public LevelEditorManager editor;
    public string _name;

    // Start is called before the first frame update
    void Start()
    {
      
    }
    private void Update()
    {
        curPosition = this.gameObject.transform.position;
        curRotation = this.gameObject.transform.rotation.eulerAngles;
    }
    void OnMouseOver() {
        editor = GameObject.FindGameObjectWithTag("Level Editor Manager").GetComponent<LevelEditorManager>();
        if(editor != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
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
                this.GetComponent<Transform>().Translate(-1, 0, 0, Space.World);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                this.GetComponent<Transform>().Translate(1, 0, 0, Space.World);
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                this.GetComponent<Transform>().Translate(0, 1, 0, Space.World);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                this.GetComponent<Transform>().Translate(0, -1, 0, Space.World);
            }
            
        }
        else
        {
            return;
        }
    }
}

