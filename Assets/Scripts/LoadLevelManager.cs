using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelManager : MonoBehaviour
{

    public static LoadLevelManager Instance;
    public GetLevelDetails levelLoader;
    public string levelName;
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
}
