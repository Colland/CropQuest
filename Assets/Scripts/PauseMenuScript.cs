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

    public void Home(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }

    public void muteVolume()
    {
        float value;
        audioMixer.GetFloat("Volume", out value);
        if (value == 0f)
        {
            audioMixer.SetFloat("Volume", -80.0f);
        }
        else
        {
            audioMixer.SetFloat("Volume", 0f);
        }

    }
}
