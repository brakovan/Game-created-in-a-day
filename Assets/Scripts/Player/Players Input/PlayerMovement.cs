using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    public Camera mainCamera;
    public PlayerMovementStatsInGame inGameMovementStats;
    public bool canMove = true, canRotate = true;
    public float speed, rotationSpeed;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private HPManager hpManager;
    private void Awake()
    {
        inGameMovementStats = GetComponent<PlayerMovementStatsInGame>();
        hpManager = GetComponent<HPManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(canRotate)
        {
            RotateBody();
        }
    }

    private void FixedUpdate()
    {
        if (hpManager.currentKnockBackTimer <= 0)
        {
            rb.velocity = moveDirection * speed;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (canMove)
        {
            moveDirection = context.ReadValue<Vector2>();
        }
    }

    private void RotateBody()
    {
        Vector3 difference = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 90;
        Quaternion rot = Quaternion.AngleAxis(rotZ, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
    }
}
