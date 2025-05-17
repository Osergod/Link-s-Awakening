using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Source")]

    [Header("- Player:")]
    public AudioClip linkHurt;
    public AudioClip linkDie;
    public AudioClip linkFall;
    public AudioClip linkJump;
    public AudioClip linkLand;

    [Header("- Enemies:")]
    public AudioClip enemyHit;
    public AudioClip enemyDie;
    public AudioClip enemyFall;
    public AudioClip bossHit;
    public AudioClip bossDie;

    [Header("- Map Objects:")]
    public AudioClip doorSlam;

    [Header("- Items:")]
    public AudioClip swordSwing;

    private static AudioManager audioManager;

    public static AudioManager instance
    {
        get {
            return RequestAudioManager();
        }
    }

    private static AudioManager RequestAudioManager()
    {
        if (!audioManager)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }
        return audioManager;
    }

    private void Start()
    {
        //musicSource.clip = doorSlam;
        //musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
