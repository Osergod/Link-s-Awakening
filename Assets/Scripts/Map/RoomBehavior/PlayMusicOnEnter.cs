using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicOnEnter : MonoBehaviour
{
    private MoldormController boss;

    private void Start()
    {
        boss = GetComponentInChildren<MoldormController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AudioManager.instance.ReplaceMusicPlaying(AudioManager.instance.bossTheme);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !boss.GetIsDead())
        {
            AudioManager.instance.ResumePreviousMusic();
        }
        else if (collision.tag == "Player" && boss.GetIsDead())
        {
            AudioManager.instance.PlayMusic(AudioManager.instance.instrumentTheme);
        }
    }
}
