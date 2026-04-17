using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Security;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

public class AudioManager : MonoBehaviour
{
    public AudioClip songToPlay;

    
    [Range(0, 100)]
    public int volume;

    public AudioSource audioSource;

    [SerializeField] GetLevelDetails lm;

    // Start is called before the first frame update
    public void GetPlayer(UnityEngine.Component sender, params object[] data)
    {
        if(sender is StartEditor && LoadLevelManager.Instance == null)
        {
            audioSource = (AudioSource)data[0];
            lm = (GetLevelDetails)data[1];
            StartCoroutine(GetAudio(Application.streamingAssetsPath + "/custom-music/", $"{lm.songName}"));
        }
        if(sender is MovePlayer && LoadLevelManager.Instance != null)
        {
            audioSource = sender.GetComponent<AudioSource>();
            StartCoroutine(GetAudio(Application.streamingAssetsPath + "/custom-music/", $"{LoadLevelManager.Instance.songName}"));
        }
        
    }

    private IEnumerator GetAudio(string path, string name)
    {
       using(UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + path + $"{name}.mp3", AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            songToPlay = DownloadHandlerAudioClip.GetContent(www);
            audioSource.clip = songToPlay;
            audioSource.Play();
        }
    }
}
