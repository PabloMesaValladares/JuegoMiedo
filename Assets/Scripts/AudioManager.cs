using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] music, sfx;
    public AudioSource musicSource, sfxSource;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in music)
        {
            s.clip = Resources.Load<AudioClip>("Music/" + s.name);
        }

        foreach (Sound s in sfx)
        {
            s.clip = Resources.Load<AudioClip>("SFX/" + s.name);
        }
    }

    public void Start()
    {
        PlayMusic("Prueba");
    }

    public void PlayMusic(string name)
    {
        Sound s = System.Array.Find(music, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        musicSource.clip = s.clip;
        musicSource.Play();
    }

    public void PlaySFX(string name)
    {
        Sound s = System.Array.Find(sfx, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        sfxSource.clip = s.clip;
        sfxSource.Play();
    }
}
