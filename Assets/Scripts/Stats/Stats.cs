using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//[CreateAssetMenu(fileName = "NewStats", menuName = "Stats/DestructibleStats", order = 1)]
public abstract class Stats : MonoBehaviour
{
    public float maxHp;
    public float armor;
    public float currentHp;
    public float speed;
    public float rotSpeed;
    public float drag;
    public Events.OnDestroy onDestroy;

    [SerializeField]
    private Faction faction;

    public Faction _Faction
    {
        get { return faction; }
    }

    public virtual void DecreaseHp(float amount)
    {
        currentHp -= amount;
        if (currentHp <= 0)
            GetDestroyed();
    }

    public virtual void IncreaseHp(float amount)
    {
        currentHp += amount;
        if (currentHp >= maxHp)
            currentHp = maxHp;
    }

    public virtual void GetDestroyed()
    {
        onDestroy.Invoke();
    }
}

public enum Faction
{
    PLAYER,
    PIRATE,
    SPACEOBJECT
}
