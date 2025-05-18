using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] TMP_Text kills;
    [SerializeField] TMP_Text time;
    [SerializeField] TMP_Text score;
    [SerializeField] TMP_Text rupees;
    [SerializeField] TMP_Text keys;
    [SerializeField] TMP_Text bombs;

    private float initialTime;
    private float timerTime;

    private int minutes, seconds, cents;

    private void Start()
    {
        timerTime = initialTime;
    }

    private void Update()
    {
        kills.text = GameManager.instance.GetPlayerKills().ToString();
        score.text = GameManager.instance.GetScore().ToString();
        keys.text = GameManager.instance.GetKeys().ToString();
        bombs.text = GameManager.instance.GetBombs().ToString();
        rupees.text = GameManager.instance.GetRupees().ToString();

        timerTime += Time.deltaTime;
        if(timerTime < 0)
        {
            timerTime = 0;
        }

        minutes = (int)(timerTime / 60f);
        seconds = (int)(timerTime - minutes * 60f);
        cents = (int)((timerTime - (int)timerTime) * 100f);

        time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
