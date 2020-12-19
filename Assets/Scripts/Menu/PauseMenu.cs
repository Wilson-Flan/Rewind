using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    public static bool GameIsPaused = false;

    public AudioMixer audioMixer;

    public Slider volumeSlider;
    public float volumeLevel;
    void Awake()
    {
        audioMixer.GetFloat("volume", out volumeLevel);
        volumeSlider.value = volumeLevel;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && GameIsPaused)
        {
            Resume();
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && !GameIsPaused)
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        GameIsPaused = true;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void ExitLevel()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("Menu");
    }
}
