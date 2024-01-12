using UnityEngine;
using UnityEngine.Events;

public class ProjectileMovement : MonoBehaviour
{
    public float maxSpeed;
    [Header("DONT TOUCH IN EDITOR")]
    public float currentSpeed;
    private void OnEnable()
    {
        currentSpeed = maxSpeed;
    }

    private void Update()
    {
        MoveProjectile();
    }

    public void DestroyBullet()
    {
        gameObject.SetActive(false);
    }

    private void MoveProjectile()
    {
        transform.Translate(currentSpeed * Time.deltaTime * Vector2.up);
    }
}
