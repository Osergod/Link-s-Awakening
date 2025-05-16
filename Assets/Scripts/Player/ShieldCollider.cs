using System.Collections.Generic;
using UnityEngine;

public class ShieldCollider : MonoBehaviour
{
    // Almacena los enemigos actualmente dentro del área del escudo
    private HashSet<GameObject> enemiesInside = new HashSet<GameObject>();

    // Verifica si un enemigo específico está dentro del escudo
    public bool IsEnemyInside(GameObject enemy) => enemiesInside.Contains(enemy);

    // Registra enemigos que entran en el área del escudo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemiesInside.Add(collision.gameObject);
        }
    }

    // Elimina enemigos que salen del área del escudo
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemiesInside.Remove(collision.gameObject);
        }
    }
}