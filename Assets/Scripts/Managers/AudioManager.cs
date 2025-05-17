using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Source")]
    public AudioClip doorSlam;

    private void Start()
    {
        musicSource.clip = doorSlam;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {

    }
}
