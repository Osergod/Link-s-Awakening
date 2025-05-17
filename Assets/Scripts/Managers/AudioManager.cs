using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clips")]

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
    public AudioClip roomSolved;
    public AudioClip oneWayDoor;
    public AudioClip chestOpen;
    public AudioClip groundCrumble;
    public AudioClip rockShatter;
    public AudioClip rockPush;
    public AudioClip stairs;
    public AudioClip error;

    [Header("- Items:")]
    public AudioClip swordSwing;

    [Header("- Music:")]
    public AudioClip dungeonTheme;

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
        
        musicSource.clip = dungeonTheme;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
