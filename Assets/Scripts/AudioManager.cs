using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource SFXSource;

    [Header("Audio Clips")]
    public AudioClip harvest;
    public AudioClip plant;
    public AudioClip walkGrass;
    public AudioClip walkWood;
    public AudioClip intro;

    public void playSFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
