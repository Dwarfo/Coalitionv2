using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateStats : Stats {


    public enum Difficulty
    {
        EASY,
        MEDIUM,
        HARD
    }

    public GameObject explosion;

    public override void GetDestroyed()
    {
        Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
        base.GetDestroyed();
        Destroy(gameObject);
    }


}
