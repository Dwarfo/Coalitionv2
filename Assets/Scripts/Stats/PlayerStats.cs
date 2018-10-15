using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Stats
{
    public float maxShield;
    public float currentShield;
    public float shieldRegenRate;
    public Animator shieldAnim;

    [SerializeField]
    private bool shieldIsBroken = false;
    [SerializeField]
    public bool shieldIsActive;

    public Events.OnDestroy onShieldDestroyed;
    public Events.OnPlayerDamageReceived onHpChanged;
    public Events.OnPlayerDamageReceived onShieldChanged;
    public Events.OnDestroy onShieldRepaired;

    private void Start()
    {
        gameObject.GetComponent<PlayerController>().onShieldActivated.AddListener(HandleShieldActivated);
        if (shieldAnim == null)
            shieldAnim = gameObject.transform.Find("Shield").GetComponent<Animator>();
    }

    private void Update()
    {
        IncreaseShield(shieldRegenRate);
    }

    public virtual void DecreaseShield(float amount)
    {
        currentShield -= amount;
        if (currentShield <= 0)
        {
            onShieldDestroyed.Invoke();
            shieldAnim.SetBool("ShieldState", false);
            shieldIsBroken = true;
            shieldIsActive = false;
        }
        onShieldChanged.Invoke(currentShield);
    }

    public virtual void IncreaseShield(float amount)
    {
        currentShield += amount;
        if (currentShield >= maxShield)
            currentShield = maxShield;

        if (currentShield >= 0.7f * maxShield && shieldIsBroken)
        {
            shieldIsBroken = false;
            if(shieldIsActive)
                shieldAnim.SetBool("ShieldState", true);
            onShieldRepaired.Invoke();
        }

        onShieldChanged.Invoke(currentShield);
    }

    public override void IncreaseHp(float amount)
    {
        base.IncreaseHp(amount);
        onHpChanged.Invoke(currentHp);
    }

    public override void DecreaseHp(float amount)
    {
        if (shieldIsActive && !shieldIsBroken)
        {
            DecreaseShield(amount);
            return;
        }
        base.DecreaseHp(amount);
        onHpChanged.Invoke(currentHp);
    }

    private void HandleShieldActivated()
    {

        if (!shieldIsActive)
        {
            shieldIsActive = true;
            shieldAnim.SetBool("ShieldState", true);
            //shieldAnim.Play("ShieldAnim");
            if (shieldIsBroken)
                shieldAnim.SetBool("ShieldState", false);
            //shieldAnim.Play("ShieldClosing");
        }
        else
        {
            shieldIsActive = false;
            shieldAnim.SetBool("ShieldState", false);
            //shieldAnim.Play("ShieldClosing");
            Debug.Log("STH");
        }
            
        

    }
  
}
