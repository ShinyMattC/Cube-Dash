using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelManager : MonoBehaviour
{

    public static LoadLevelManager Instance;
    public GetLevelDetails levelLoader;
    public string levelName;
    public string songName;
    public GameObject player;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
    }
    private void Update()
    {
        if(SceneManager.GetActiveScene().ToString() == "LevelEditor")
        {
           Destroy(gameObject); 
        }
    }
    
}
