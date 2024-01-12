using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform player;
    public float attackRange = 5.0f; // Диапазон атаки
    public float attackInterval = 2.0f; // Интервал между атаками в секундах
    private float timeSinceLastAttack;

    private void Awake()
    {
        timeSinceLastAttack = attackInterval;
    }
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= attackRange)
        {
            // Если игрок в пределах диапазона атаки
            if (timeSinceLastAttack >= attackInterval)
            {
                AttackPlayer();
                timeSinceLastAttack = 0f;
            }
            else
            {
                timeSinceLastAttack += Time.deltaTime;
            }
        }
    }

    void AttackPlayer()
    {
        // Здесь реализация атаки на игрока
        Debug.Log("Атака игрока");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
