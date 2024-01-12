using UnityEngine;
using System.Collections.Generic;

public class PointerController : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 10f;
    public LayerMask targetLayer;
    public float rotationSpeed = 5f;
    public float stopDistance = 2f;
    public float goneFarDistance = 2f;
    [SerializeField] private SpriteRenderer pointerSprite;
    [SerializeField] private PointerController[] otherPointerControllers;
    private Transform target;
    private List<Transform> objectsIFound = new List<Transform>();

    void Update()
    {
        if (target != null)
        {
            PointAtTarget();
        }
        else
        {
            FindNearestTarget();
        }
    }

    void FindNearestTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, targetLayer);

        float nearestDistance = float.MaxValue;
        Transform nearestTarget = null;

        foreach (var collider in colliders)
        {
            //if (collider.GetComponent<ProvideHeat>())
            //{
                if (collider.GetComponent<ProvideHeat>() && collider.GetComponent<ProvideHeat>().isWarm && !FoundObjectsManager.Instance.IsObjectFound(collider.transform))
                {
                    bool targetInUse = false;

                    foreach (var otherController in otherPointerControllers)
                    {
                        if (otherController != this && otherController.objectsIFound.Contains(collider.transform))
                        {
                            targetInUse = true;
                            break;
                        }
                    }

                    if (!targetInUse)
                    {
                        float distanceToTarget = Vector2.Distance(transform.position, collider.transform.position);
                        if (distanceToTarget < nearestDistance)
                        {
                            nearestDistance = distanceToTarget;
                            nearestTarget = collider.transform;
                        }
                    }
                }
                Debug.Log(collider.name + " " + collider.GetComponent<ProvideHeat>().isWarm);
            //}
        }
        if (nearestTarget != null)
        {
            target = nearestTarget;
            pointerSprite.gameObject.SetActive(true);
            objectsIFound.Add(target);
            FoundObjectsManager.Instance.AddFoundObject(target);
        }
    }

    void PointAtTarget()
    {
        if (Vector2.Distance(target.position, transform.position) > goneFarDistance)
        {
            objectsIFound.Remove(target);
            FoundObjectsManager.Instance.RemoveFoundObject(target);
            target = null;
            pointerSprite.gameObject.SetActive(false);
            return;
        }

        Vector2 directionToTarget = target.position - transform.position;

        if (directionToTarget.magnitude > stopDistance)
        {
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            objectsIFound.Remove(target);
            FoundObjectsManager.Instance.RemoveFoundObject(target);

            for (int i = 0; i < otherPointerControllers.Length; i++)
            {
                otherPointerControllers[i].objectsIFound.Add(target);
                FoundObjectsManager.Instance.AddFoundObject(target);
            }
            target = null;
            pointerSprite.gameObject.SetActive(false);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
