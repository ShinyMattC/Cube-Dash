using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemController : MonoBehaviour
{
    public int id;
    public int quantity = 9999999;
    public TextMeshProUGUI quantityText;

    public bool clicked;
    private LevelEditorManager editor;
    // Start is called before the first frame update
    void Start()
    {
        quantityText.text = quantity.ToString();
        editor = GameObject.FindGameObjectWithTag("Level Editor Manager").GetComponent<LevelEditorManager>();
    }
    public void ButtonClick()
    {
        if(quantity > 0)
        {
            Vector3 screenPosition = Input.mousePosition;
            screenPosition.z = 10;
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            Instantiate(editor.itemImage[id], new Vector3(worldPosition.x, worldPosition.y, 10), Quaternion.identity);
            clicked = true;
            quantity--;
            quantityText.text = quantity.ToString();
            editor.curButtonPressed = id;
        }
    }

    
}
