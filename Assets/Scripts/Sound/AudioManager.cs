using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : Persistance<AudioManager> 
{

    [SerializeField] private Sound[] musics, sfxs;
    [SerializeField] private AudioSource musicSource, sfxSource;

    public AudioSource MusicSource { get => musicSource; set => musicSource = value; }
    public AudioSource SfxSource { get => sfxSource; set => sfxSource = value; }

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        PlayMusic("StartScene");
    }
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musics, x => x.name == name);
        if (s != null)
        {
            MusicSource.clip = s.clip;
            MusicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(musics, x => x.name == name);
        if (s != null)
        {
            SfxSource.PlayOneShot(s.clip);
        }
    }

    public bool ToggleMusic()
    {
        MusicSource.mute = !MusicSource.mute;
        return MusicSource.mute;
    }

    public bool ToggleSFX()
    {
        SfxSource.mute = !SfxSource.mute;
        return SfxSource.mute;
    }

    public void AdjustMusicVolume(float volume)
    {
        MusicSource.volume = volume;
    }

    public void AdjustSFXVolume(float volume)
    {
        SfxSource.volume = volume;
    }
}
