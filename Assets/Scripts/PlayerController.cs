using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody2D Player;
    public float acceleration = 10f;
    public PlayerStats playerStats;
    public Weapon currentWeapon;

    private bool mode = false;
    private int dragValue = 10;
    private Vector2 direction;
    private Transform firingPosition;

    // Use this for initialization
    void Start ()
    {
        currentWeapon = gameObject.GetComponent<Weapon>();
        Player = gameObject.GetComponent<Rigidbody2D>();
        playerStats = gameObject.GetComponent<PlayerStats>();
        if (firingPosition == null)
            firingPosition = gameObject.transform.Find("FirePosition");
    }
	
	// Update is called once per frame
	void Update ()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal") * acceleration, Input.GetAxisRaw("Vertical") * acceleration);
        Fly();
        Rotate();

        if (Input.GetMouseButtonDown(0) && currentWeapon != null)
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

    private void Rotate()
    {
        int xAngle = -(int)transform.rotation.x;
        int yAngle = -(int)transform.rotation.y;
        int angle = (int)transform.rotation.z;

        //Depending on input compute "Z" Vector rotation value
        if (direction.x > 0) xAngle = 270;
        if (direction.x < 0) xAngle = 90;
        if (direction.y < 0) yAngle = 180;
        if (direction.y > 0)
        {
            if (direction.x > 0) yAngle = 360;
            else yAngle = 0;
        }

        if (direction.x == 0 && direction.y != 0)
            angle = yAngle;
        if (direction.y == 0 && direction.x != 0)
            angle = xAngle;

        if (direction.x != 0 && direction.y != 0)
        {
            angle = (xAngle + yAngle) / 2;
        }
        //Make sure you don't rotate when there is no input
        if (direction != Vector2.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * playerStats.rotSpeed);

    }

    private Vector2 ClampVelocity(Vector2 velocity)
    {
        float x = Mathf.Clamp(Player.velocity.x, -playerStats.speed, playerStats.speed);
        float y = Mathf.Clamp(Player.velocity.y, -playerStats.speed, playerStats.speed);

        return new Vector2(x, y);
    }
}
