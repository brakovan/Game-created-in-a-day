using UnityEngine;
using UnityEngine.Events;

public class HPManager : MonoBehaviour
{
    public float coldnessMultiplier, warmthGoingUp;
    public float intervalOfColdnessDamage; // интервал нанесения холодом урона
    public int coldnessDamage;
    public float knockbackForce;
    [SerializeField] private bool disableIt, exterminate;
    [Header("DONT TOUCH IN EDITOR")]
    public float currentColdnessLevel;
    public bool canFreeze = false;
    private float lastColdnessDamageTime;
    public int currentHp;
    public float maxColdnessLevel;
    private Rigidbody2D rb;
    [HideInInspector] public bool beingKnocked;
    public float knockBackTimer;
    [HideInInspector] public float currentKnockBackTimer;
    private void Start()
    {
        if(GetComponent<Rigidbody2D>()) rb = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        if(currentKnockBackTimer > 0)
        {
            currentKnockBackTimer -= Time.deltaTime;
        }
        if (canFreeze) //может мерзнуть
        {
            if (currentColdnessLevel < maxColdnessLevel)
            {
                currentColdnessLevel += Time.deltaTime * coldnessMultiplier; // потихоньку мерзнет
            }
            else
            {
                currentColdnessLevel = maxColdnessLevel;
                FrostbiteProcess();
            }
        }
        else
        {
            currentColdnessLevel -= Time.deltaTime * warmthGoingUp;
        }
    }

    public void TakeDamage(int takenDamage, bool knockBack)
    {
        currentHp -= takenDamage;
        if (currentHp <= 0)
        {
            if (currentHp < 0) currentHp = 0;
            //deathEvent?.Invoke();
            if (disableIt)
            {
                gameObject.SetActive(false);
            }
            if (exterminate)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (knockBack)
            {
                // Apply knockback when taking damage
                ApplyKnockback();
            }
        }
    }

    public void FrostbiteProcess()
    {
        if (Time.time > lastColdnessDamageTime + intervalOfColdnessDamage)
        {
            // Apply damage
            TakeDamage(coldnessDamage, false);

            // Update the last damage time
            lastColdnessDamageTime = Time.time;
        }
    }

    private void ApplyKnockback()
    {
        currentKnockBackTimer = knockBackTimer;
        Debug.Log("Knocked");
        // You can adjust the force and direction based on your needs
        Vector2 currentKnockbackForce = transform.up.normalized * -knockbackForce; // Example force
        rb.AddForce(currentKnockbackForce, ForceMode2D.Impulse);
    }
}
