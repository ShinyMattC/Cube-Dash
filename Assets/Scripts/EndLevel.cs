using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public void GoToScene(Component sender, params object[] data)
    {
        if((string)data[3] == this.tag)
        {
            SceneManager.LoadScene((int)data[4]);
        }
    }
}
