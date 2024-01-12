using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float attackRadius = 5f; // ������ ����������� ������
    public float stopDistance = 1f; // ��������� ���������

    private Rigidbody2D rb;
    public Transform player; // ������� ������� �����
    private bool inZone = false;
    public LayerMask currentLayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        FindPlayerInRadius();
        Movement();
    }

    void FindPlayerInRadius()
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, attackRadius, currentLayer);
        if (hitCollider != null)
        {
            inZone = true;
        }
        else
        {
            inZone = false;
        }
    }

    void Movement()
    {
        if (inZone && player != null)
        {
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);
            if (distanceToPlayer > stopDistance)
            {
                Vector3 directionToPlayer = player.position - transform.position;
                Vector3 normalizedDirection = directionToPlayer.normalized;
                rb.velocity = new Vector2(normalizedDirection.x * moveSpeed, normalizedDirection.y * moveSpeed);

                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
            else
            {
                rb.velocity = Vector2.zero; // ������������� �����
            }
        }
        else
        {
            rb.velocity = Vector2.zero; // ������������� �����, ���� ����� ����� �� ����
        }
    }

    // ����������� ������� ����������� � ��������� ��������� � ��������� Unity
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}
