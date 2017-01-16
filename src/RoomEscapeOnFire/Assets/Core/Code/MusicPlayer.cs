using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Yarn.Unity;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip RelaxedSong;
    public AudioClip PanicSong;
    public AudioClip EpilogueSong;
    public AudioClip PizzaSong;
    public AudioClip PoliceSong;
    public AudioClip BeeSong;
    public AudioClip DogSong;


    public AudioSource MusicSource;

    void Start()
    {
        MusicSource = GetComponent<AudioSource>();
    }

    private AudioClip GetSongToPlay(string songName)
    {       
        // Yes I know this would work better in a dictionary, get off my back mom
        if (songName == "relaxed")
        {
            return RelaxedSong;
        }
        if (songName == "panic")
        {
            return PanicSong;
        }
        if (songName == "epilogue")
        {
            return EpilogueSong;
        }
        if (songName == "police")
        {
            return PoliceSong;
        }
        if (songName == "pizza")
        {
            return PizzaSong;
        }
        if (songName == "bee")
        {
            return BeeSong;
        }
        if (songName == "dog")
        {
            return DogSong;
        }


        return null;
    }

    // relaxed
    // panic
    [YarnCommand("play")]
    public void PlaySong(string songName)
    {
        AudioClip songToPlay = GetSongToPlay(songName);

        MusicSource.loop = true;
        if (MusicSource.isPlaying)
        {
            StartCoroutine(PlaySongAndWaitForFadeInOut(songToPlay));
        }
        else
        {
            MusicSource.volume = .5f;
            MusicSource.clip = songToPlay;
            MusicSource.Play();
        }
    }

    [YarnCommand("play-oneshot")]
    public void PlaySongNoLoop(string songName)
    {
        AudioClip songToPlay = GetSongToPlay(songName);

        MusicSource.loop = false;
        if (MusicSource.isPlaying)
        {
            StartCoroutine(PlaySongAndWaitForFadeInOut(songToPlay));
        }
        else
        {
            MusicSource.volume = .5f;
            MusicSource.clip = songToPlay;
            MusicSource.Play();
        }
    }

    private IEnumerator PlaySongAndWaitForFadeInOut(AudioClip song)
    {
        yield return MusicSource.DOFade(0f, .25f).WaitForCompletion();

        MusicSource.Stop();
        MusicSource.clip = song;
        MusicSource.Play();
        MusicSource.DOFade(.5f, .125f);
    }

    [YarnCommand("stop")]
    public void StopSong()
    {
        MusicSource.DOFade(0f, .25f);
    }
}
