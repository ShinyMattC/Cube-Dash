using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song : MonoBehaviour
{
    
    //Song class for easier song integration

    public AudioClip song;
    public int length;
    public string title;
    public string author;

    public Song(AudioClip song, int length, string title, string author) {
        this.song = song;
        this.length = length;
        this.title = title;
        this.author = author;
    }
    
}
