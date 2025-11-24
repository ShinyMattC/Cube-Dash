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
    public int[] rotation = new int[4];
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

    public string levelName;

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
            GetMetadata(Application.persistentDataPath + "/custom-levels/", $"{levelSave.levelName}.txt");

        }
        if(Input.GetKeyDown(KeyCode.Insert))
        {
            GetLevelBlockDetails(Application.persistentDataPath + "/custom-levels/", $"{levelSave.levelName}.txt");
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

        foreach(string s in details)
        {
            int found = s.IndexOf("§");
            Debug.Log($"{s.Substring(0, found)}");
        }
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
                            Debug.Log($"{blockMetadata.Count}");
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
                int.TryParse(str[7], out b.rotation[3]);
                // b.DisplayBlockDetails(b);
                GameObject blockGO;

                // Instantiate gameobjects, with positions declared in Block class
                /*GameObject block = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(b.position[0], b.position[1], b.position[2]), Quaternion.identity);
                block.AddComponent<BoxCollider>();
                block.AddComponent<BlockItem>();
                BlockItem bi = block.GetComponent<BlockItem>();
                switch (b.blockName)
                {

                    case "Block":
                        blockGO = Instantiate(prefabs[0], new Vector3(b.position[0], b.position[1], b.position[2]),new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]));
                        blockGO.GetComponent<BlockItem>().curPosition = new Vector3(b.position[0], b.position[1], b.position[2]);
                        blockGO.GetComponent<BlockItem>().curRotation = new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]);
                        levelSave.OnBlockPlaced(blockGO.GetComponent<BlockItem>());
                        break;
                    case "Spike":
                        blockGO = Instantiate(prefabs[1], new Vector3(b.position[0], b.position[1], b.position[2]),new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]));
                        blockGO.GetComponent<BlockItem>().curPosition = new Vector3(b.position[0], b.position[1], b.position[2]);
                        blockGO.GetComponent<BlockItem>().curRotation = new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]);
                        levelSave.OnBlockPlaced(prefabs[1].GetComponent<BlockItem>());
                        break;
                    case "JOrb":
                        blockGO = Instantiate(prefabs[2], new Vector3(b.position[0], b.position[1], b.position[2]),new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]));
                        blockGO.GetComponent<BlockItem>().curPosition = new Vector3(b.position[0], b.position[1], b.position[2]);
                        blockGO.GetComponent<BlockItem>().curRotation = new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]);
                        levelSave.OnBlockPlaced(prefabs[2].GetComponent<BlockItem>());
                        break;
                    case "BPortal":
                        blockGO = Instantiate(prefabs[3], new Vector3(b.position[0], b.position[1], b.position[2]),new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]));
                        blockGO.GetComponent<BlockItem>().curPosition = new Vector3(b.position[0], b.position[1], b.position[2]);
                        blockGO.GetComponent<BlockItem>().curRotation = new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]);
                        levelSave.OnBlockPlaced(prefabs[3].GetComponent<BlockItem>());
                        break;
                    case "BGPortal":
                        blockGO = Instantiate(prefabs[4], new Vector3(b.position[0], b.position[1], b.position[2]),new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]));
                        blockGO.GetComponent<BlockItem>().curPosition = new Vector3(b.position[0], b.position[1], b.position[2]);
                        blockGO.GetComponent<BlockItem>().curRotation = new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]);
                        levelSave.OnBlockPlaced(prefabs[4].GetComponent<BlockItem>());
                        break;
                    case "SGPortal":
                        blockGO = Instantiate(prefabs[5], new Vector3(b.position[0], b.position[1], b.position[2]),new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]));
                        blockGO.GetComponent<BlockItem>().curPosition = new Vector3(b.position[0], b.position[1], b.position[2]);
                        blockGO.GetComponent<BlockItem>().curRotation = new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]);
                        levelSave.OnBlockPlaced(prefabs[5].GetComponent<BlockItem>());
                        break;
                    case "CGPortal":
                        blockGO = Instantiate(prefabs[6], new Vector3(b.position[0], b.position[1], b.position[2]),new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]));
                        blockGO.GetComponent<BlockItem>().curPosition = new Vector3(b.position[0], b.position[1], b.position[2]);
                        blockGO.GetComponent<BlockItem>().curRotation = new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]);
                        levelSave.OnBlockPlaced(prefabs[6].GetComponent<BlockItem>());
                        break;
                    case "SpawnPoint":
                        blockGO = Instantiate(prefabs[7], new Vector3(b.position[0], b.position[1], b.position[2]),new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]));
                        blockGO.GetComponent<BlockItem>().curPosition = new Vector3(b.position[0], b.position[1], b.position[2]);
                        blockGO.GetComponent<BlockItem>().curRotation = new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]);
                        levelSave.OnBlockPlaced(prefabs[7].GetComponent<BlockItem>());
                        break;
                    case "SpeedNormal":
                        blockGO = Instantiate(prefabs[8], new Vector3(b.position[0], b.position[1], b.position[2]),new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]));
                        blockGO.GetComponent<BlockItem>().curPosition = new Vector3(b.position[0], b.position[1], b.position[2]);
                        blockGO.GetComponent<BlockItem>().curRotation = new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]);
                        levelSave.OnBlockPlaced(prefabs[8].GetComponent<BlockItem>());
                        break;
                    case "SpeedFast":
                        blockGO = Instantiate(prefabs[9], new Vector3(b.position[0], b.position[1], b.position[2]),new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]));
                        blockGO.GetComponent<BlockItem>().curPosition = new Vector3(b.position[0], b.position[1], b.position[2]);
                        blockGO.GetComponent<BlockItem>().curRotation = new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]);
                        levelSave.OnBlockPlaced(prefabs[9].GetComponent<BlockItem>());
                        break;
                    case "SpeedVFast":
                        blockGO = Instantiate(prefabs[10], new Vector3(b.position[0], b.position[1], b.position[2]),new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]));
                        blockGO.GetComponent<BlockItem>().curPosition = new Vector3(b.position[0], b.position[1], b.position[2]);
                        blockGO.GetComponent<BlockItem>().curRotation = new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]);
                        levelSave.OnBlockPlaced(prefabs[10].GetComponent<BlockItem>());
                        break;
                    case "SpeedSlow":
                        blockGO = Instantiate(prefabs[11], new Vector3(b.position[0], b.position[1], b.position[2]),new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]));
                        blockGO.GetComponent<BlockItem>().curPosition = new Vector3(b.position[0], b.position[1], b.position[2]);
                        blockGO.GetComponent<BlockItem>().curRotation = new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]);
                        levelSave.OnBlockPlaced(prefabs[11].GetComponent<BlockItem>());
                        break;
                    case "SpeedVSlow":
                        blockGO = Instantiate(prefabs[12], new Vector3(b.position[0], b.position[1], b.position[2]),new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]));
                        blockGO.GetComponent<BlockItem>().curPosition = new Vector3(b.position[0], b.position[1], b.position[2]);
                        blockGO.GetComponent<BlockItem>().curRotation = new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]);
                        levelSave.OnBlockPlaced(prefabs[12].GetComponent<BlockItem>());
                        break;
                    case "LevelEnd":
                        blockGO = Instantiate(prefabs[13], new Vector3(b.position[0], b.position[1], b.position[2]),new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]));
                        blockGO.GetComponent<BlockItem>().curPosition = new Vector3(b.position[0], b.position[1], b.position[2]);
                        blockGO.GetComponent<BlockItem>().curRotation = new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]);
                        levelSave.OnBlockPlaced(prefabs[13].GetComponent<BlockItem>());
                        break;
                    case "Slope30":
                        blockGO = Instantiate(prefabs[14], new Vector3(b.position[0], b.position[1], b.position[2]), new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]));
                        blockGO.GetComponent<BlockItem>().curPosition = new Vector3(b.position[0], b.position[1], b.position[2]);
                        blockGO.GetComponent<BlockItem>().curRotation = new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]);
                        levelSave.OnBlockPlaced(prefabs[14].GetComponent<BlockItem>());
                        break;
                    case "Slope45":
                        blockGO = Instantiate(prefabs[15], new Vector3(b.position[0], b.position[1], b.position[2]), new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]));
                        blockGO.GetComponent<BlockItem>().curPosition = new Vector3(b.position[0], b.position[1], b.position[2]);
                        blockGO.GetComponent<BlockItem>().curRotation = new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]);
                        levelSave.OnBlockPlaced(prefabs[15].GetComponent<BlockItem>());
                        break;

                }*/
                //Instantiate(prefabs[Array.IndexOf(prefabs, b.blockName)], new Vector3(b.position[0], b.position[1], b.position[2]), new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]));
                blockGO = Instantiate(prefabs[Array.IndexOf(defaultBlockNames, b.blockName)], new Vector3(b.position[0], b.position[1], b.position[2]), new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]));
                blockGO.AddComponent<BlockItem>();
                blockGO.GetComponent<BlockItem>().curPosition = new Vector3(b.position[0], b.position[1], b.position[2]);
                blockGO.GetComponent<BlockItem>().curRotation = new Quaternion(b.rotation[0], b.rotation[1], b.rotation[2], b.rotation[3]);
                levelSave.OnBlockPlaced(blockGO.GetComponent<BlockItem>());
        }
        
        
    }
    /*#region No one needs to know
    public Tuple<Dictionary<string, int>, Dictionary<string, Vector3>> TestPlaceBlocks()
    {
        LevelSave ls = GetComponent<LevelSave>();
        Tuple<Dictionary<string, int>, Dictionary<string, Vector3>> blockDetails;
        Dictionary<string, int> blockIdMet = new Dictionary<string, int>();
        Dictionary<string, Vector3> blockPosMet = new Dictionary<string, Vector3>();

        foreach (int i in ls.blockIDs)
        {
            int blockIDIndex = 0;
            foreach (Vector3 v in ls.blockPositions)
            {
                int blockPosIndex = 0;
                blockIdMet.Add($"id{blockIDIndex}:", i);
                blockPosMet.Add($"id{blockPosIndex}:", v);
                blockPosIndex++;
            }
            blockIDIndex++;
        }
        blockDetails = new Tuple<Dictionary<string, int>, Dictionary<string, Vector3>>(blockIdMet, blockPosMet);
        return blockDetails;
    }
    #endregion */
    public Tuple<List<string>, List<Vector3>> TestPlaceBlocks()
    {
        LevelSave ls = GetComponent<LevelSave>();
        Tuple<List<string>, List<Vector3>> blockDetails;
        List<string> blockNames = new List<string>();
        List<Vector3> blockPoss = new List<Vector3>();

        foreach(string s in ls.blockNames)
        {
            foreach(Vector3 v in ls.blockPositions)
            {
                blockNames.Add(s);
                
                blockPoss.Add(v);
                
            }
        }
        blockDetails = new Tuple<List<string>, List<Vector3>>(blockNames, blockPoss);
        return blockDetails;

    }
    public void LoadLevel()
    {
        LoadLevelManager.Instance.levelLoader = this;
        if (inp_levelName != null) { LoadLevelManager.Instance.levelName = inp_levelName.text; } 
        DontDestroyOnLoad(transform.root.gameObject);
        SceneManager.LoadSceneAsync(9);

    }
    public void LoadLevel(string levelName)
    {
        LoadLevelManager.Instance.levelLoader = this;
        if (inp_levelName != null) { LoadLevelManager.Instance.levelName = levelName; }
        DontDestroyOnLoad(transform.root.gameObject);
        SceneManager.LoadSceneAsync(9);

    }
}
