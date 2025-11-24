using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Song songToPlay;
    
    [Range(0, 100)]
    public int volume;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.PlayOneShot(songToPlay.song, volume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
