using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMusicScript : MonoBehaviour
{
    [SerializeField] private AudioClip Clip;

    private void Awake()
    {
        //switch clip
        GameObject Audio = GameObject.FindGameObjectWithTag("Audio");
        Audio.GetComponent<AudioSource>().clip = Clip;
        Audio.GetComponent<MusicScript>().StopMusic();
        Audio.GetComponent<MusicScript>().PlayMusic();
    }
}
