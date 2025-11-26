using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ChangeGameQuality : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    // Start is called before the first frame update
    void Start()
    {
        dropdown.value = PlayerPrefs.GetInt("graphicsTier");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeGraphics(int index) {
        QualitySettings.SetQualityLevel(index);
        Debug.Log(QualitySettings.GetQualityLevel());
        PlayerPrefs.SetInt("graphicsTier", QualitySettings.GetQualityLevel());
    }
}
