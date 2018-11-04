using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats_BSMN : Stats {

    public Events.OnPlayerDamageReceived onHpChanged;
    public GameObject explosion;


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

    public override void GetDestroyed()
    {
        Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
        base.GetDestroyed();
        gameObject.SetActive(false);
    }
}
