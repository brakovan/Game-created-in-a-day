using UnityEngine;
using UnityEngine.Events;

public class BulletBehaviour : MonoBehaviour
{
    public int damage;
    public float distance, maxActiveTime;
    [SerializeField] private LayerMask whatIsHittable;

    [HideInInspector] public Transform owner;
    private RaycastHit2D hitInfo;
    private GameObject whatIHit;
    private float currentActiveTime;

    private void OnEnable()
    {
        currentActiveTime = maxActiveTime;
        whatIHit = null;
    }

    private void Update()
    {
        currentActiveTime -= Time.deltaTime;
        if (currentActiveTime <= 0)
        {
            TurnOffBullet();
        }
        SimpleBullet();
    }

    public void SimpleBullet() // simple bullets
    {
        hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsHittable);
        if (hitInfo.collider != null)
        {
            whatIHit = hitInfo.collider.gameObject;
            if (whatIHit.GetComponent<HPManager>())
            {
                whatIHit.GetComponent<HPManager>().TakeDamage(damage, true);
                //if (whatIHit.transform.root.GetComponent<EnemyBehaviour>())
                {
                    //TriggerEnemyToAttack(whatIHit.transform.root.GetComponent<EnemyBehaviour>());
                }
            }
            TurnOffBullet();
        }
    }

    /*private void TriggerEnemyToAttack(EnemyBehaviour enemyBehaviour)
    {
        enemyBehaviour.target = owner;
        enemyBehaviour.eCurrentBehaviour = enemyBehaviour.eChasePlayers;
    }*/

    public void TurnOffBullet()
    {
        gameObject.SetActive(false);
    }
}
