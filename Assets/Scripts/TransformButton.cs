using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum editorbuttontype
    {
        MoveLeft,
        MoveRight,
        MoveUp,
        MoveDown,
        RotateRight45,
        RotateLeft45,
        FlipH,
        FlipV,
        Delete
    }
public class TransformButton : MonoBehaviour
{
    
    public editorbuttontype Editorbuttontype;
    public EventSO onEditorButtonPressed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Sendbutton()
    {
        onEditorButtonPressed.raise(this, Editorbuttontype);
    }
}
