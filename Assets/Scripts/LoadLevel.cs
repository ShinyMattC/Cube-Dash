using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    private void Start()
    {
        if(LoadLevelManager.Instance != null)
        {
            GetLevelDetails gld = LoadLevelManager.Instance.levelLoader;
// test
            gld.GetLevelBlockDetails(Application.persistentDataPath + "/custom-levels/", $"{LoadLevelManager.Instance.levelName}.txt");
            gld.PlaceBlocks(gld.parsedDetails);
            Destroy(GameObject.Find("Canvas"));
            Instantiate(LoadLevelManager.Instance.player, GameObject.FindGameObjectWithTag("Spawn Point").transform.position, Quaternion.identity);
        }
    }
}
