using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponInHandControl : MonoBehaviour
{
    public Weapon currentWeapon;

    public void ShootPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(currentWeapon.canShoot)
            currentWeapon.startShooting = true;
        }
        else if (context.canceled)
        {
            currentWeapon.startShooting = false;
        }
    }
}
