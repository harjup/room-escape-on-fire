using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperTextSound : MonoBehaviour
{
    private AudioSource _audioSource;

    public AudioClip Fridge;
    public AudioClip Bang;
    public AudioClip Knock;
    public AudioClip Blanket;
    public AudioClip Explosion;

    public void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void DoEvent(string s, TextInfo info)
    {
        if (s == "fridge")
        {
            _audioSource.PlayOneShot(Fridge, .75f);
        }
        else if (s == "bang")
        {
            _audioSource.PlayOneShot(Bang, .25f);
        }
        else if (s == "knock")
        {
            _audioSource.PlayOneShot(Knock, 1f);
        }
        else if (s == "blanket")
        {
            _audioSource.PlayOneShot(Blanket, 1f);
        }
        else if (s == "explosion")
        {
            _audioSource.PlayOneShot(Explosion, .5f);
        }
    }
}