using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private string previousScene;

    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    private AudioClip previousMusicSource;

    [Header("Audio Clips")]

    [Header("- Player:")]
    public AudioClip linkHurt;
    public AudioClip linkDie;
    public AudioClip linkFall;
    public AudioClip linkJump;
    public AudioClip linkLand;
    public AudioClip lowHealth;

    [Header("- Enemies:")]
    public AudioClip enemyHit;
    public AudioClip enemyDie;
    public AudioClip enemyFall;
    public AudioClip enemyNoHit;
    public AudioClip bossHit;
    public AudioClip bossDie;
    public AudioClip bossExplode;

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
    public AudioClip burst;

    [Header("- Items:")]
    public AudioClip swordSwing;
    public AudioClip useShield;
    public AudioClip getItem;
    public AudioClip getRupee;
    public AudioClip getInstrument;
    public AudioClip bombExplosion;

    [Header("- Music:")]
    public AudioClip dungeonTheme;
    public AudioClip houseTheme;
    public AudioClip villageTheme;
    public AudioClip bossTheme;
    public AudioClip playerSelectTheme;
    public AudioClip introTheme;
    public AudioClip instrumentTheme;

    [Header("- Menu:")]
    public AudioClip textLetter;
    public AudioClip menuCursor;
    public AudioClip menuSelect;

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

    private void Awake()
    {
        musicSource.loop = true;

        if (audioManager != null && audioManager != this)
        {
            Destroy(gameObject);
            return;
        }

        audioManager = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != previousScene)
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "MainMenu":
                    musicSource.clip = introTheme;
                    break;
                case "SampleScene":
                    musicSource.clip = villageTheme;
                    break;
                case "Dungeon1":
                    musicSource.clip = dungeonTheme;
                    break;
            }

            previousScene = SceneManager.GetActiveScene().name;
            musicSource.Play();
            previousMusicSource = musicSource.clip;
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void ReplaceMusicPlaying(AudioClip clip)
    {
        previousMusicSource = musicSource.clip;
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void ResumePreviousMusic()
    {
        musicSource.clip = previousMusicSource;
        musicSource.Play();
    }
}
