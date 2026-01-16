using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicOnEnter : MonoBehaviour
{
    private bool isPlaying = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isPlaying)
        {
            isPlaying = true;
            AudioManager.instance.ReplaceMusicPlaying(AudioManager.instance.houseTheme);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlaying = false;
            AudioManager.instance.ResumePreviousMusic();
        }
    }
}
