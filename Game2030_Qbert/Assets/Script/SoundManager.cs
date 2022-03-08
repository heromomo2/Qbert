using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private GameObject music_souce, effects_source;

    public Sound[]  effect_sounds;

    public Sound music_sound;

    public static SoundManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else 
        {
            Destroy(this);
        }

        foreach (Sound es in effect_sounds)
        {
            if( effects_source != null) 
            {
                es.Source = effects_source.AddComponent<AudioSource>();
                es.Source.clip = es.Clip;
                es.Source.volume = es.volume;
                es.Source.loop = es.loop;
            }
        }

        if (music_souce != null) 
        {
            if (music_sound != null) 
            {
                music_sound.Source = music_souce.AddComponent<AudioSource>();
                music_sound.Source.clip = music_sound.Clip;
                music_sound.Source.volume = music_sound.volume;
                music_sound.Source.loop = music_sound.loop; 
            }
        }
    }


    public void PlaySoundEffect(string name)
    {
       Sound seached_effect = Array.Find(effect_sounds, sound => sound.name == name);
        if (seached_effect != null)
        {
            seached_effect.Source.Play();
        }
        else 
        {
            Debug.LogWarning("SoundEffect: "+ name +" not found!");
        }
    }

    public void PlayMusic()
    {
        if (music_sound != null) 
        {
            music_sound.Source.Play();
        }
    }

    public void StopMusic()
    {
        if (music_sound != null)
        {
            music_sound.Source.Stop();
        }
    }



    //public void playSoundEffet(AudioClip audio_clip)
    //{
    //    effects_source.PlayOneShot(audio_clip);
    //}

    //public void playSoundMusic(AudioClip audio_clip)
    //{
    //    music_souce.Play(audio_clip);
    //}
}
