using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Yarn.Unity;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip RelaxedSong;
    public AudioClip PanicSong;

    public AudioSource MusicSource;

    void Start()
    {
        MusicSource = GetComponent<AudioSource>();
    }

    // relaxed
    // panic
    [YarnCommand("play")]
    public void PlaySong(string songName)
    {
        AudioClip songToPlay = null;
        if (songName == "relaxed")
        {
            songToPlay = RelaxedSong;
        }
        else if (songName == "panic")
        {
            songToPlay = PanicSong;
        }

        if (MusicSource.isPlaying)
        {
            StartCoroutine(PlaySongAndWaitForFadeInOut(songToPlay));
        }
        else
        {
            MusicSource.DOFade(.5f, .25f);
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
        MusicSource.DOFade(.5f, .25f);
    }

    [YarnCommand("stop")]
    public void StopSong()
    {
        MusicSource.DOFade(0f, .25f);
    }
}
