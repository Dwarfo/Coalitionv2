using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BarControls : MonoBehaviour {

    public Image hpBar;
    public Image shieldBar;
    public Image shieldsImg;
    public Image stateImg;

    public Sprite flyingState;
    public Sprite attackState;
    public Sprite brokenShield;
    public Sprite functionalShield;

    private float hpMaxValue;
    private float shieldMaxValue;

	void Start ()
    {
        WorldGenerator.Instance.onPlayerReady.AddListener(HandlePlayerReady);
        stateImg.sprite = flyingState;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void HandlePlayerReady(GameObject player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        PlayerController controls = player.GetComponent<PlayerController>();

        hpMaxValue = stats.maxHp;
        shieldMaxValue = stats.maxShield;
        stats.onHpChanged.AddListener(HandleHpChanged);
        stats.onShieldChanged.AddListener(HandleShieldChanged);
        stats.onShieldDestroyed.AddListener(HandleShieldDestroyed);
        stats.onShieldRepaired.AddListener(HandleShieldRepaired);
        controls.onBattleStateChanged.AddListener(HandleBattleStateChanged);
        controls.onShieldActivated.AddListener(HandleShieldActivated);

    }

    private void HandleHpChanged(float amount)
    {
        hpBar.fillAmount = amount / hpMaxValue;
    }

    private void HandleShieldChanged(float amount)
    {
        shieldBar.fillAmount = amount / shieldMaxValue;
    }

    private void HandleBattleStateChanged(BattleState state)
    {
        if (state == BattleState.Attacking)
            stateImg.sprite = attackState;
        else
            stateImg.sprite = flyingState;
    }

    private void HandleShieldActivated()
    {
        shieldsImg.gameObject.SetActive(!shieldsImg.IsActive()); 
    }

    private void HandleShieldDestroyed()
    {
        shieldsImg.gameObject.SetActive(false); 
        shieldsImg.sprite = brokenShield;
    }

    private void HandleShieldRepaired()
    {
        shieldsImg.sprite = functionalShield;
    }
}
