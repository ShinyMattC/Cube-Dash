using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class Block
{
    public string blockName;
    public int[] position = new int[3];
    public int[] rotation = new int[3];
    public void DisplayBlockDetails(Block b)
    {
        Debug.Log("name: " + b.blockName + " pos: ");
        for(int j = 0; j < b.position.Length; j++)
        {
            Debug.Log($"{b.position[j]}");
        }
    }
}
public class GetLevelDetails : MonoBehaviour
{
    public LevelSave levelSave;
    public List<string> parsedDetails = new List<string>();
    public string[] defaultBlockNames = { "Block", "Spike", "JOrb", "BPortal", "BGPortal", "SGPortal", "CGPortal", "SpawnPoint"  };
    public GameObject[] prefabs = { };
    public GameObject player;
    public TMP_InputField inp_levelName;
    public TMP_InputField inp_songName;

    public string levelName;
    public string songName;


    // Start is called before the first frame update
    void Start()
    {
        levelSave = this.GetComponent<LevelSave>();
        
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            //PlaceBlocks(string.Empty);
            GetMetadata(Application.streamingAssetsPath + "/custom-levels/", $"{levelSave.levelName}.txt");

        }
        if(Input.GetKeyDown(KeyCode.Insert))
        {
            GetLevelBlockDetails(Application.streamingAssetsPath + "/custom-levels/", $"{levelSave.levelName}.txt");
            PlaceBlocks(parsedDetails);
        }
        if(Input.GetKeyDown(KeyCode.F2))
        {
            PlaceBlocks(parsedDetails);
            
        }
    }
    public void GetMetadata(string levelPath, string levelName)
    {
        string levelTxt = levelPath + levelName;

        string[] details = File.ReadAllLines(levelTxt);
        Dictionary<string, string> metadata = new Dictionary<string, string>();
        string[] seperator = {": "};

        foreach(string s in details)
        {
            if(s.Contains("§"))
            {
                string[] temp = s.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                temp[1] = temp[1].Substring(0, temp[1].IndexOf('§'));
                //Debug.Log($"{temp[0]} {temp[1]}");
                metadata.Add(temp[0], temp[1]);
                Debug.Log(metadata.Count);
                foreach(KeyValuePair<string, string> kvp in metadata)
                {
                    if(levelSave.inp_levelName != null)
                    {
                        switch(kvp.Key)
                        {
                            case "name":
                                levelSave.inp_levelName.text = kvp.Value;
                                break;
                            case "author":
                                levelSave.inp_levelAuthor.text = kvp.Value;
                                break;
                            case "songName":
                                levelSave.inp_songName.text = kvp.Value;
                                break;
                            case "songAuthor":
                                levelSave.inp_songAuthor.text = kvp.Value;
                                break;
                        }
                    }
                    else
                    {
                        switch(kvp.Key)
                        {
                            case "name":
                                levelName = kvp.Value;
                                break;
                            case "songName":
                                songName = kvp.Value;
                                break;
                                
                    }
                    }
                }
            }
            

        }
        this.levelName = metadata["name"];
        this.songName = metadata["songName"];
    }
    public void GetLevelBlockDetails(string levelPath, string levelName)
    {
        string levelTxt = levelPath + levelName;

        string[] details = File.ReadAllLines(levelTxt);

        List<string> blockMetadata = new List<string>();

        foreach(string s in details)
        {
            if(!s.Contains("§"))
            {
                if(!s.Contains("{"))
                {
                    if(!s.Contains("}"))
                    {
                        {
                            print($"{s}");
                            blockMetadata.Add(s);
                            //Debug.Log($"{blockMetadata.Count}");
                        }
                    }
                }
                
            }
            
        }
        parsedDetails = blockMetadata;
        
    }
    public void PlaceBlocks(List<string> blockDetails)
    {
        List<int> positions = new List<int>();
        foreach (string s in blockDetails)
        {
               if (s == null)
            {
                break;
            } 
                // Decompose string, then put data into Block class
                char[] seperators = { '(', ',', ')', ';' };

                string[] str = s.Split(seperators);
                Block b = new Block();
                b.blockName = str[0];
                int.TryParse(str[1], out b.position[0]);
                int.TryParse(str[2], out b.position[1]);
                int.TryParse(str[3], out b.position[2]);
                int.TryParse(str[4], out b.rotation[0]);
                int.TryParse(str[5], out b.rotation[1]);
                int.TryParse(str[6], out b.rotation[2]);
                
                GameObject blockGO;

                // Instantiate gameobjects, with positions declared in Block class
                
                blockGO = Instantiate(prefabs[Array.IndexOf(defaultBlockNames, b.blockName)], new Vector3(b.position[0], b.position[1], b.position[2]), Quaternion.Euler(b.rotation[0], b.rotation[1], b.rotation[2]));
                blockGO.AddComponent<BlockItem>();
                blockGO.GetComponent<BlockItem>().curPosition = new Vector3(b.position[0], b.position[1], b.position[2]);
                blockGO.GetComponent<BlockItem>().curRotation = new Vector3(b.rotation[0], b.rotation[1], b.rotation[2]);
                levelSave.OnBlockPlaced(blockGO.GetComponent<BlockItem>());
        }
        
        
    }
    
    
    public void LoadLevel()
    {
        LoadLevelManager.Instance.levelLoader = this;
        if (inp_levelName != null) { 
            LoadLevelManager.Instance.levelName = inp_levelName.text;
            LoadLevelManager.Instance.songName = inp_songName.text; 
        }
        else
        {
            LoadLevelManager.Instance.levelName = levelName;
            LoadLevelManager.Instance.songName = songName; 
        } 
        DontDestroyOnLoad(transform.root.gameObject);
        SceneManager.LoadSceneAsync(9);

    }
    public void LoadLevel(string levelName)
    {
        LoadLevelManager.Instance.levelLoader = this;
        LoadLevelManager.Instance.levelName = levelName;

        DontDestroyOnLoad(transform.root.gameObject);
        
        SceneManager.LoadSceneAsync(9);

    }
    public void simulatebuttonpress()
    {
        GetMetadata(Application.streamingAssetsPath + "/custom-levels/", $"{levelSave.levelName}.txt");
        LoadLevel();
    }
}
