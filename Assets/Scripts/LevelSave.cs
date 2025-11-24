using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;
using UnityEditor;

public class LevelSave : MonoBehaviour
{
    private string levelPth;
    public string levelName;
    public string levelAuthor;
    public string songName;
    public string songAuthor;
    public List<BlockItem> blockItems;

    public TMP_InputField inp_levelName;
    public TMP_InputField inp_levelAuthor;
    public TMP_InputField inp_songName;
    public TMP_InputField inp_songAuthor;

    public List<int> blockIDs;
    public List<Vector3> blockPositions;
    public List<Quaternion> blockRotations;
    public List<string> blockNames;

    private LevelEditorManager lm;
    // Start is called before the first frame update
    void Start()
    {
        blockItems = new List<BlockItem>();
        blockIDs = new List<int>();
        blockPositions = new List<Vector3>();
        lm = GameObject.Find("LevelEditorManager").GetComponent<LevelEditorManager>();
        levelPth = Application.persistentDataPath + "/custom-levels/";
        if(!Directory.Exists(levelPth))
        {
            Directory.CreateDirectory(levelPth);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            SaveLevel();
        }
        levelName = inp_levelName.text;
        levelAuthor = inp_levelAuthor.text;
        songName = inp_songName.text;
        songAuthor = inp_songAuthor.text;
        
    }
    private void Awake()
    {
        
    }
    public void SaveLevel()
    {

        BlockItem[] blockArray = ConvertToArray(blockItems);
        if (!File.Exists(levelPth + levelName + ".txt"))
        {
            
            WriteToTxt("name: " + levelName + "§");
            WriteToTxt("author: " + levelAuthor + "§");
            WriteToTxt("songName: " + songName + "§");
            WriteToTxt("songAuthor: " + songAuthor + "§");
            WriteToTxt("levelData: {");
            foreach (BlockItem b in blockArray)
            {
                WriteToTxt($"{b._name}({(int)b.curPosition.x},{(int)b.curPosition.y},{(int)b.curPosition.z};{(int)b.curRotation.x},{(int)b.curRotation.y},{(int)b.curRotation.z},{(int)b.curRotation.w})");
                blockIDs.Add(b.id);
                blockPositions.Add(b.curPosition);
                blockRotations.Add(b.curRotation);
                blockNames.Add(b._name);

                Debug.Log($"{blockIDs.Count}");
                Debug.Log($"{blockPositions.Count}");
            }
            WriteToTxt("}");
        }
        else
        {
            blockIDs.Clear();
            blockPositions.Clear();
            blockNames.Clear();
            blockRotations.Clear();
            ClearContentsOfSaveFile(levelPth + levelName + ".txt"); //To remove writing all over existing file

            WriteToTxt("name: " + levelName + "§");
            WriteToTxt("author: " + levelAuthor + "§");
            WriteToTxt("songName: " + songName + "§");
            WriteToTxt("songAuthor: " + songAuthor + "§");
            WriteToTxt("levelData: {");
            foreach (BlockItem b in blockArray)
            {
                WriteToTxt($"{b._name}({(int)b.curPosition.x},{(int)b.curPosition.y},{(int)b.curPosition.z};{(int)b.curRotation.x},{(int)b.curRotation.y},{(int)b.curRotation.z},{(int)b.curRotation.w})");
                blockIDs.Add(b.id);
                blockPositions.Add(b.curPosition);
                blockRotations.Add(b.curRotation);
                blockNames.Add(b._name);

                Debug.Log($"{blockIDs.Count}");
                Debug.Log($"{blockPositions.Count}");
                Debug.Log($"{blockNames.Count}");
            }
            WriteToTxt("}");
        }
        
        
    }
    public void WriteToTxt(string stuffToWrite)
    {
        StreamWriter author = new StreamWriter(levelPth + levelName + ".txt", true);
        author.WriteLine(stuffToWrite);
        author.Close();
    #if UNITY_EDITOR
        AssetDatabase.Refresh();
    #endif
    }
    public void ClearContentsOfSaveFile(string path)
    {
        FileStream fs = File.Open(path, FileMode.Open);

        fs.SetLength(0);
        fs.Close();
    }
    public void OnBlockPlaced(BlockItem block) 
    {
        blockItems.Add(block);
        blockIDs.Add(block.id);
        blockPositions.Add(block.curPosition);
        blockRotations.Add(block.curRotation);
        blockNames.Add(block._name);
    }
    public void OnBlockRemoved(BlockItem block)
    {
        blockItems.Remove(block);
        blockIDs.Remove(block.id);
        blockPositions.Remove(block.curPosition);
        blockRotations.Remove(block.curRotation);
        blockNames.Add(block._name);
    }
    private BlockItem[] ConvertToArray(List<BlockItem> bloc)
    {
        return bloc.ToArray();
    }
    public void SaveBlocksAndPutIntoSaveList(List<BlockItem> b)
    {
        ConvertToArray(b);
    }
}
