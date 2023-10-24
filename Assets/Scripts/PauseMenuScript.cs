using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public AudioMixer audioMixer;
    public AudioSource BGMusic;
    public void pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void saveScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SavingScene");
    }

    public void Home(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }

    public void muteMusicVolume()
    {
        float value;
        audioMixer.GetFloat("Music", out value);
        if (value == 0f)
        {
            audioMixer.SetFloat("Music", -80.0f);
        }
        else
        {
            audioMixer.SetFloat("Music", 0f);
        }

    }

    public void muteSFXVolume()
    {
        float value;
        audioMixer.GetFloat("SFX", out value);
        if (value == 0f)
        {
            audioMixer.SetFloat("SFX", -80.0f);
        }
        else
        {
            audioMixer.SetFloat("SFX", 0f);
        }

    }
}
