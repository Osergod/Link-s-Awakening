using System.Collections.Generic;
using UnityEngine;

public class ShieldCollider : MonoBehaviour
{
    private HashSet<GameObject> enemiesInside = new HashSet<GameObject>();

    public bool IsEnemyInside(GameObject enemy)
    {
        return enemiesInside.Contains(enemy);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemiesInside.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemiesInside.Remove(collision.gameObject);
        }
    }
}
