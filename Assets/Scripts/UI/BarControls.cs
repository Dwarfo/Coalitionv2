using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BarControls : MonoBehaviour {

    public Image hpBar;
    public Image shieldBar;

    private float hpMaxValue;
    private float shieldMaxValue;

	void Start ()
    {
        WorldGenerator.Instance.onPlayerReady.AddListener(HandlePlayerReady);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void HandlePlayerReady(GameObject player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();

        hpMaxValue = stats.maxHp;
        shieldMaxValue = stats.maxShield;
        stats.onHpChanged.AddListener(HandleHpChanged);
        stats.onShieldChanged.AddListener(HandleShieldChanged);
    }

    private void HandleHpChanged(float amount)
    {
        hpBar.fillAmount = amount / hpMaxValue;
    }

    private void HandleShieldChanged(float amount)
    {
        shieldBar.fillAmount = amount / shieldMaxValue;
    }
}
