using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorBehaviour : MonoBehaviour {

	public MeteorStats meteorStats;

    [SerializeField]
    private bool isSmall;

	void Start ()
    {
        meteorStats = gameObject.GetComponent<MeteorStats>();
        meteorStats.onDestroy.AddListener(HandleDestroy);
	}
	
	// Update is called once per frame
	void Update ()
    {
        //meteorStats.DecreaseHp(0.1f);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public void HandleDestroy()
    {
        if (!isSmall)
        {
            GameObject[] partMeteor = new GameObject[2];

            for (int i = 0; i < partMeteor.Length; i++)
            {
                partMeteor[i] = Instantiate(meteorStats.smallerMeteor, gameObject.transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, Random.Range(0, 180)));
            }

            foreach (GameObject meteor in partMeteor)
            {
                meteor.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5, 5), Random.Range(-5, 5)));
            }
            
        }

        Destroy(gameObject);
    }

}
