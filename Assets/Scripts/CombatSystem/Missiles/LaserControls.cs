using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControls : MonoBehaviour {

    public GameObject explosion;

    private Rigidbody2D rb;
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float destroingDelay;

    void Start ()
    {
        gameObject.layer = 9;
        rb = gameObject.GetComponent<Rigidbody2D>();
        Destroy(gameObject, destroingDelay);
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
        //projectileNotificator = gameObject.GetComponent<ProjectileNotificator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (explosion != null)
        {
            GameObject expl = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
        }
    }

}
