using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    //private AudioSource source;

    private AudioSource soundSource;
    private AudioSource musicSource;


    private void Awake()
    {

        instance = this;
        soundSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        //keep this object even where we go to new scene
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null && instance!= this) 
            Destroy(gameObject);
    }

    public void PlaySound(AudioClip _sound)
    { 
    
        soundSource.PlayOneShot(_sound);
    
    }

    public void ChangeSoundVolume(float _change)
    {
        
        float currentVolume = PlayerPrefs.GetFloat("soundVolume");  //loads last saved sound volume from player prefs
        currentVolume += _change;


        //checks if we reached the maximum or minimum value
        if (currentVolume > 1)
        {
            currentVolume = 0;
        }
        else if (currentVolume < 0)
        {
            currentVolume = 1;
        }


        //assigns final value
        soundSource.volume = currentVolume;

        //save
        PlayerPrefs.GetFloat("soundVolume", currentVolume);
    }
    public void ChangeMusicVolume(float _change)
    {
        
        float currentVolume = PlayerPrefs.GetFloat("musicVolume");  //loads last saved sound volume from player prefs
        currentVolume += _change;


        //checks if we reached the maximum or minimum value
        if (currentVolume > 1)
        {
            currentVolume = 0;
        }
        else if (currentVolume < 0)
        {
            currentVolume = 1;
        }


        //assigns final value
        musicSource.volume = currentVolume;

        //save
        PlayerPrefs.GetFloat("musicVolume", currentVolume);
    }

}
