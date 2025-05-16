using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private StatsManager stats;
    [SerializeField] private TMP_InputField PlayerName;
    [SerializeField] private LinkDatabase db;

    void Start()
    {
        stats = FindObjectOfType<StatsManager>();
    }

    public void OnPlayerName()
    {
        stats.namePlayer = PlayerName.text;
        Debug.Log("Nombre guardado: " + stats.namePlayer);
        db.UpdateDatabase();
    }
}
