using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private Sound[] musicSounds, sfxSounds;
    [SerializeField] private AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //PlayMusic("Background");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name); //Funcion para buscar en una array previamente definida según un parametro

        if (s == null)
        {
            Debug.Log("No existe el sonido con nombre: " + name);
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play(); //Recordemos poner Loop en el musicSource si es de BG
        }
    }

    public void PlaySfx(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("No existe el sfx con nombre: " + name);
        }
        else
        {
            sfxSource.PlayOneShot(s.clip); //Solo una vez
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSfx()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
