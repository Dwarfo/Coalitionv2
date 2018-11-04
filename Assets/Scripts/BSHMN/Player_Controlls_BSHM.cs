using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controlls_BSHM : MonoBehaviour {


    public Rigidbody2D Player;
    public float acceleration = 50f;
    public PlayerStats_BSMN playerStats;
    public Weapon currentWeapon;

    private int dragValue = 10;
    private Vector2 direction;
    private Transform firingPosition;

	// Use this for initialization
	void Start ()
    {
        currentWeapon = gameObject.GetComponent<Weapon>();
        Player = gameObject.GetComponent<Rigidbody2D>();
        playerStats = gameObject.GetComponent<PlayerStats_BSMN>();
        if (firingPosition == null)
            firingPosition = gameObject.transform.Find("FirePosition");
    }
	
	// Update is called once per frame
	void Update ()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal") * acceleration, Input.GetAxisRaw("Vertical") * acceleration);


        Fly();

        if (Input.GetKey(KeyCode.Space) && currentWeapon != null)
        {
            currentWeapon.FireProjectile(firingPosition);
        }

    }

    private void Fly()
    {
        Player.AddForce(direction);
        Player.velocity = ClampVelocity(Player.velocity);

        if (Player.velocity.x != 0 && direction.x == 0 && direction.y != 0)
            Player.velocity = new Vector2(Mathf.Lerp(Player.velocity.x, 0, 0.2f), Player.velocity.y);
        if (Player.velocity.y != 0 && direction.y == 0 && direction.x != 0)
            Player.velocity = new Vector2(Player.velocity.x, Mathf.Lerp(Player.velocity.y, 0, 0.2f));

        if (direction == Vector2.zero)
            Player.drag = dragValue;
        else
            Player.drag = 0;

        //Debug.Log("Direction: " + direction + " Velocity: " + Player.velocity);
    }

    private Vector2 ClampVelocity(Vector2 velocity)
    {
        float x = Mathf.Clamp(Player.velocity.x, -playerStats.speed, playerStats.speed);
        float y = Mathf.Clamp(Player.velocity.y, -playerStats.speed, playerStats.speed);

        return new Vector2(x, y);
    }
}
