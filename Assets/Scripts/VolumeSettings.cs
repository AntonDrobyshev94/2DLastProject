using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private Slider sliderVolumeMusic;
    [SerializeField] private Slider sliderVolumeSound;
    [SerializeField] private AudioSource audioMusic;
    [SerializeField] private AudioSource audioSound;
    [SerializeField] private float volumeMusic;
    [SerializeField] private float volumeSound;
    [SerializeField] private float voluneSound2;
    [SerializeField] private AudioScript audioScript;
    private float pausedTime = 0.0f;

    private void Start()
    {
        
    }

    private void Awake()
    {
        LoadMusicVolume();
        LoadSoundVolume();
        ValueMusic();
        ValueSound();
    }

    public void SliderMusic()
    {
        volumeMusic = sliderVolumeMusic.value;
        SaveMusicVolume();
        ValueMusic();
    }

    public void SliderSound()
    {
        volumeSound = sliderVolumeSound.value;
        SaveSoundVolume();
        ValueSound();
    }

    private void ValueMusic()
    {
        audioMusic.volume = volumeMusic;

        sliderVolumeMusic.value = volumeMusic;
    }

    private void ValueSound()
    {
        audioSound.volume = volumeSound;

        for (int i = 0; i < audioScript.audioSoundsArray.Length; i++) //цикл, который задает уровень громкости всем объектам массива AudioScriptArray
        {
            audioScript.audioSoundsArray[i].volume = volumeSound;
        }

        sliderVolumeSound.value = volumeSound;
    }

    private void SaveMusicVolume()
    {
        PlayerPrefs.SetFloat("volumeMusic", volumeMusic);
    }

    private void LoadMusicVolume()
    {
        volumeMusic = PlayerPrefs.GetFloat("volumeMusic", volumeMusic);
    }

    private void SaveSoundVolume()
    {
        PlayerPrefs.SetFloat("volumeSound", volumeSound);
    }

    private void LoadSoundVolume()
    {
        volumeSound = PlayerPrefs.GetFloat("volumeSound", volumeSound);
    }

    public void PauseGameMusic()
    {
        pausedTime = audioMusic.time;
        audioMusic.Stop();
    }

    public void PlayGameMusic()
    {
        audioMusic.Play();
        audioMusic.time = pausedTime;
    }
}
