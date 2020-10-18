using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;
    public bool playing;

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioSource _globalSFX1;
    [SerializeField] AudioSource _globalSFX2;
    [SerializeField] AudioSource _globalSFX3;

    private void Awake()
    {
        #region Singleton Pattern (Simple)
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
    }

    public void PlaySong(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
        playing = true;
    }

    public void PlaySFX(AudioClip clip, int channel)
    {
        if (channel == 1)
        {
            _globalSFX1.clip = clip;
            _globalSFX1.Play();
        }
        if (channel == 2)
        {
            _globalSFX2.clip = clip;
            _globalSFX2.Play();
        }
        if (channel == 3)
        {
            _globalSFX3.clip = clip;
            _globalSFX3.Play();
        }
    }

    public void StopSFX(int channel)
    {
        if (channel == 1)
        {
            _globalSFX1.Stop();
        }
        if (channel == 2)
        {
            _globalSFX2.Stop();
        }
        if (channel == 3)
        {
            _globalSFX3.Stop();
        }
    }
}