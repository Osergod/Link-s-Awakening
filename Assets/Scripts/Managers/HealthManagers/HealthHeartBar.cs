using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHeartBar : MonoBehaviour
{
    public GameObject heartPrefab;
    public PlayerHealth playerHealth;
    List<HealthHeart> hearts = new List<HealthHeart>();

    // Maneja la vida que tiene el jugador en relación al evento de daño
    private void OnEnable() => PlayerHealth.OnPlayedDamaged += DrawHearts;
    private void OnDisable() => PlayerHealth.OnPlayedDamaged -= DrawHearts;

    // Inicializa la barra de vida
    private void Start() => DrawHearts();

    // Actualiza la barra de vida según la vida actual
    public void DrawHearts()
    {
        ClearHearts();
        float maxHealthRemainder = playerHealth.maxHealth % 2;
        int heartsToMake = (int)((playerHealth.maxHealth / 2) + maxHealthRemainder);
        for (int i = 0; i < heartsToMake; i++) CreateEmptyHeart();

        for (int i = 0; i < hearts.Count; i++)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(playerHealth.health - (i * 2), 0, 2);
            hearts[i].SetHeartImage((HealthHeart.HeartStatus)heartStatusRemainder);
        }
    }

    // Crea un corazón vacío
    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);
        HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
        heartComponent.SetHeartImage(HealthHeart.HeartStatus.Empty);
        hearts.Add(heartComponent);
    }

    // Elimina todos los corazones de la barra de vida
    public void ClearHearts()
    {
        foreach (Transform t in transform) Destroy(t.gameObject);
        hearts = new List<HealthHeart>();
    }
}