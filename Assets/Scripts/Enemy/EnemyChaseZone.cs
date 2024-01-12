using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseZone : MonoBehaviour
{
    public Transform player;
    public GameObject[] enemies; // Массив GameObjects врагов
    public float moveSpeed = 5.0f; // Скорость преследования
    public float stopDistance = 2.0f; // Дистанция остановки от игрока
    private Vector2[] startPositions; // Массив для хранения исходных позиций врагов
    private bool isPlayerInChaseZone = false;

    private void Awake()
    {
        startPositions = new Vector2[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            // Сохраняем исходную позицию каждого врага
            startPositions[i] = enemies[i].transform.position;
        }
    }

    void Update()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            GameObject enemyObject = enemies[i];
            Rigidbody2D rb = enemyObject.GetComponent<Rigidbody2D>();

            Vector3 directionToTarget = (isPlayerInChaseZone ? player.position : (Vector3)startPositions[i]) - enemyObject.transform.position;
            float distanceToTarget = directionToTarget.magnitude;

            if (distanceToTarget > stopDistance)
            {
                MoveEnemy(rb, directionToTarget.normalized * moveSpeed);
            }
            else
            {
                rb.velocity = Vector2.zero; // Останавливаем врага
            }
        }
    }

    private void MoveEnemy(Rigidbody2D rb, Vector2 velocity)
    {
        rb.velocity = velocity;
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90;
        rb.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == player)
        {
            isPlayerInChaseZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == player)
        {
            isPlayerInChaseZone = false;
        }
    }
}


