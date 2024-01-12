using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ObjectPool poolOfBullets;
    [SerializeField] private Transform transformShootPoint;
    public float cooldownTime = 0.5f; // Adjust the cooldown time as needed
    public float reloadTime = 2.0f; // Adjust the reload time as needed
    public int maxAmmo = 6, savedBullets = 12; // Adjust the maximum ammo count
    public bool startShooting;
    public int currentAmmo;
    private float lastShootTime;
    private bool isReloading;
    public bool canShoot = true;
    private void Start()
    {
        currentAmmo = maxAmmo;
        lastShootTime = -cooldownTime; // To allow shooting immediately
    }

    private void Update()
    {
        if (currentAmmo <= 0)
        {
            Reload();
            return;
        }
        if (currentAmmo > 0)
        {
            if (startShooting && !isReloading)
            {
                if (canShoot) Shoot();
            }
        }
    }

    public void Shoot()
    {
        // Check if there is enough ammo and if enough time has passed since the last shot
        if (currentAmmo > 0 && Time.time - lastShootTime > cooldownTime)
        {
            // Shoot a bullet
            poolOfBullets.GetProjectile(transformShootPoint.position, transformShootPoint.rotation);

            // Update the last shoot time
            lastShootTime = Time.time;

            // Decrease ammo count
            currentAmmo--;
        }
    }

    private void Reload()
    {
        startShooting = false;
        canShoot = false;
        if (savedBullets > 0)
        {
            if (!isReloading)
            {
                // Set the reloading flag to true
                isReloading = true;
                // Reload logic here
                // For simplicity, we'll set the ammo to the maxAmmo count after a delay
                Invoke("CompleteReload", reloadTime);
            }
        }
        else
        {
            Debug.Log("NO AMMO TO RELOAD");
        }
    }

    private void CompleteReload()
    {
        // Reset the reloading flag
        isReloading = false;
        if (savedBullets > 0)
        {
            int whatAmmoIGet = maxAmmo - currentAmmo;
            if (savedBullets >= whatAmmoIGet)
            {
                savedBullets -= whatAmmoIGet;
                currentAmmo += whatAmmoIGet;
            }
            else
            {
                currentAmmo = savedBullets;
                savedBullets = 0;
            }
            canShoot = true;
        }
        else
        {
            canShoot = false;
            Debug.Log("NO AMMO");
        }
    }

    public void ReloadPressed(InputAction.CallbackContext context)
    {
        if (!isReloading)
        {
            Reload();
        }
        Debug.Log("pressed R");
    }
}
