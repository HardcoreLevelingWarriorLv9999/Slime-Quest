using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public int damageToGive = 2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            EnemyHealthManager enemyHealthManager;
            enemyHealthManager = other.gameObject.GetComponent<EnemyHealthManager>();
            enemyHealthManager.HurtEnemy(damageToGive);
        }
    }
}
