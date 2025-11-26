using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    public void ButtonSceneLoad(int sceneIndex)
    {
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("graphicsTier"));
        SceneManager.LoadSceneAsync(sceneIndex);
    }

}
