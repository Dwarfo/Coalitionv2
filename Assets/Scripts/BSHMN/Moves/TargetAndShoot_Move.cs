using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAndShoot_Move : Move
{
    public Transform tr;
    public float cooldownBetweenSeries;

    private float time = 0;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Weapon weapon;
    [SerializeField]
    private Transform firepos;

    [SerializeField]
    private int shotsFired = 0;
    private float currentCd = 0;
    

    public override void makeAction(float deltaTime)
    {
        RotateTowards(tr, target);

        if (shotsFired <= 4)
        {
            weapon.FireProjectile(firepos);
        }

        if (shotsFired >= 4)
        {
            currentCd += deltaTime;
            if (currentCd > cooldownBetweenSeries)
            {
                currentCd = 0;
                shotsFired = 0;
            }
        }

        time += deltaTime;
        if (time > duration && onActionFinished != null)
            onActionFinished.Invoke(this);
    }

    public override void undoAction()
    {

    }

    public override void Initialize(GameObject go)
    {
        tr = go.GetComponent<Transform>();
        target = GameObject.Find("Player_BSHM").transform;
        weapon = go.GetComponent<Weapon>();

        weapon.onCoolDownStarted.AddListener(HandleOnCooldownStarted);

        firepos = gameObject.GetComponentInChildren<Transform>();
        this.dontUnsub = true;
        this.duration = 2f;
        Debug.Log("Initialized");
    }

    public override float GetDuration()
    {
        return duration;
    }

    public void RotateTowards(Transform character, Transform direction)
    {
        Vector3 difference = direction.position - character.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 90;
        character.rotation = Quaternion.Slerp(character.rotation, Quaternion.Euler(0f, 0f, rotation_z), Time.deltaTime * 5f);
    }

    private void HandleOnCooldownStarted(float cooldown)
    {
        shotsFired++;
    }
}
