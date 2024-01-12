using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform player;
    public float attackRange = 5.0f; // �������� �����
    public float attackInterval = 2.0f; // �������� ����� ������� � ��������
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
            // ���� ����� � �������� ��������� �����
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
        // ����� ���������� ����� �� ������
        Debug.Log("����� ������");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
