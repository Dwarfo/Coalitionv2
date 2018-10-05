using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Stats
{
    public float maxShield;
    public float currentShield;

    public Events.OnDestroy onShieldDestroyed;
    public Events.OnPlayerDamageReceived onHpChanged;
    public Events.OnPlayerDamageReceived onShieldChanged;

    public virtual void DecreaseShield(float amount)
    {
        currentShield -= amount;
        if (currentShield <= 0)
            onShieldDestroyed.Invoke();
        onShieldChanged.Invoke(currentShield);
    }

    public virtual void IncreaseShield(float amount)
    {
        currentShield += amount;
        if (currentShield >= maxShield)
            currentShield = maxShield;
        onShieldChanged.Invoke(currentShield);
    }

    public override void IncreaseHp(float amount)
    {
        base.IncreaseHp(amount);
        onHpChanged.Invoke(currentHp);
    }

    public override void DecreaseHp(float amount)
    {
        base.DecreaseHp(amount);
        onHpChanged.Invoke(currentHp);
    }


  
}
