using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StartEditor : MonoBehaviour
{
    public GameObject player;
    [SerializeField] int amountOfPlayers = 0;

    public EventSO onEditorGameStart;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame(int action)
    {
        switch (action) {
            case 0:
                Time.timeScale = 1f;
                Debug.Log("It's time");
                
                if (amountOfPlayers == 0)
                {
                    try
                    {
                        GameObject p = Instantiate(player, GameObject.FindGameObjectWithTag("Spawn Point").transform.position, Quaternion.identity); // make player
                        amountOfPlayers++;
                        TextMeshProUGUI errText = GameObject.Find("Error text").GetComponent<TextMeshProUGUI>();
                        errText.text = "";
                        GetLevelDetails gld = GameObject.FindGameObjectWithTag("Level Editor Manager").GetComponent<GetLevelDetails>();
                        gld.GetMetadata(Application.streamingAssetsPath + "/custom-levels/", $"{gld.gameObject.GetComponent<LevelSave>().levelName}.txt");
                        onEditorGameStart.raise(this, p.GetComponent<AudioSource>(), GameObject.FindGameObjectWithTag("Level Editor Manager").GetComponent<GetLevelDetails>());
                    }
                    catch
                    {
                        TextMeshProUGUI errText = GameObject.Find("Error text").GetComponent<TextMeshProUGUI>();
                        errText.text = "No player spawn point defined!";
                    }
                }
                else if (amountOfPlayers == 1)
                {
                    Destroy(GameObject.FindGameObjectsWithTag("Player")[1]);
                    amountOfPlayers--;

                }
                break;
            case 1:
                Debug.Log("The world!");
                Time.timeScale = (Time.timeScale == 1f) ? 0f : 1f;
                break;
        }
    }
}
