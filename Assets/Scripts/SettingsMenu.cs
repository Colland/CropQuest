using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;

    private void start()
    {
        setMusicVolume();
        setSFXVolume();
    }
    public void setMusicVolume()
    {
        float volume = MusicSlider.value;
        audioMixer.SetFloat("Music", volume);
    }

    public void setSFXVolume()
    {
        float volume = SFXSlider.value;
        audioMixer.SetFloat("SFX", volume);
    }
}
