using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider volumeSlider;
    public float volumeLevel;

    void Awake()
    {
        audioMixer.GetFloat("volume", out volumeLevel);
        volumeSlider.value = volumeLevel;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}
