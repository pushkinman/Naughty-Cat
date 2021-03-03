using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Image musicImage;
    public Sprite musicOn;
    public Sprite musicOff;
    
    public Image soundImage;
    public Sprite soundOn;
    public Sprite soundOff;
    
    public Image vibrationImage;
    public Sprite vibrationOn;
    public Sprite vibrationOff;
    
    private AudioSource musicAudioSource;
    private int musicPlaying;
    
    private GameObject[] soundAudioSources;
    private int soundPlaying;
    
    private GameObject vibrationAudioSource;
    private int vibrationPlaying;

    private void Start()
    {
        if(GameObject.FindGameObjectWithTag("Music") != null)
            musicAudioSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        musicPlaying = PlayerPrefs.GetInt("Music");

        soundAudioSources = GameObject.FindGameObjectsWithTag("Sound");
        soundPlaying = PlayerPrefs.GetInt("Sound");
        
        vibrationAudioSource = GameObject.FindGameObjectWithTag("Vibration");
        vibrationPlaying = PlayerPrefs.GetInt("Vibration");

        if (GameObject.FindGameObjectWithTag("Music") != null)
        {
            if(musicPlaying == 0)
            {
                MusicOn();
            }
            else
            {
                MusicOff();
            }
        }
        
        
        if(soundPlaying == 0)
        {
            SoundOn();
        }
        else
        {
            SoundOff();
        }
        
        if(vibrationPlaying == 0)
        {
            VibrationOn();
        }
        else
        {
            VibrationOff();
        }
    }

    private void MusicOn()
    {
        musicAudioSource.volume = 0.5f;
        if (musicImage != null)
            musicImage.sprite = musicOn;
        PlayerPrefs.SetInt("Music", 0);
    }

    private void MusicOff()
    {
        musicAudioSource.volume = 0;
        if (musicImage != null)
            musicImage.sprite = musicOff;
        PlayerPrefs.SetInt("Music", 1);
    }

    public void SwitchMusic()
    {
        if (musicPlaying == 0)
        {
            MusicOff();
            musicPlaying = 1;
        }
        else
        {
            MusicOn();
            musicPlaying = 0;
        }
    }

    private void SoundOn()
    {
        foreach (GameObject sound in soundAudioSources)
        {
            sound.GetComponent<AudioSource>().volume = 1;
        }
        if (soundImage != null)
            soundImage.sprite = soundOn;
        PlayerPrefs.SetInt("Sound", 0);
    }
    
    private void SoundOff()
    {
        foreach (GameObject sound in soundAudioSources)
        {
            sound.GetComponent<AudioSource>().volume = 0;
        }
        if (soundImage != null)
            soundImage.sprite = soundOff;
        PlayerPrefs.SetInt("Sound", 1);
    }

    public void SwitchSound()
    {
        if (soundPlaying == 0)
        {
            SoundOff();
            soundPlaying = 1;
        }
        else
        {
            SoundOn();
            soundPlaying = 0;
        }
    }
    
    private void VibrationOn()
    {
        Vibrate.canVibrate = true;
        if (vibrationImage != null)
            vibrationImage.sprite = vibrationOn;
        PlayerPrefs.SetInt("Vibration", 0);
    }

    private void VibrationOff()
    {
        Vibrate.canVibrate = false;
        if (vibrationImage != null)
            vibrationImage.sprite = vibrationOff;
        PlayerPrefs.SetInt("Vibration", 1);
    }
    
    public void SwitchVibration()
    {
        if (vibrationPlaying == 0)
        {
            VibrationOff();
            vibrationPlaying = 1;
        }
        else
        {
            VibrationOn();
            vibrationPlaying = 0;
        }
    }
}
