using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBossMusicOnEnter : MonoBehaviour
{
    [SerializeField] MoldormController boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !boss.GetIsDead())
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
    }
}
