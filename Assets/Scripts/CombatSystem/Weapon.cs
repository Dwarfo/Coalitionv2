using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public MisileSO currentWeapon;
    [Header("Custom cooldown")]
    [Tooltip("If Value equals -1, default cooldown value will be used")]
    public float coolDown = -1;
    public Events.OnCoolDownStarted onCoolDownStarted;
    public bool isNotAtCooldown = true;

    private void Start()
    {
        if (coolDown == -1)
            coolDown = currentWeapon.coolDown;
    }

    public virtual void FireProjectile(Transform firePos)
    {
        if (isNotAtCooldown)
        {
            Instantiate(currentWeapon.misile, firePos.position, firePos.rotation);
            ResetCooldown();
            onCoolDownStarted.Invoke(coolDown);
            Invoke("ResetCooldown", coolDown);

        }
    }

    private void ResetCooldown()
    {
        isNotAtCooldown = !isNotAtCooldown;
    }

    public virtual void EquipWeapon(MisileSO weapon)
    {
        currentWeapon = weapon;
    }
}
