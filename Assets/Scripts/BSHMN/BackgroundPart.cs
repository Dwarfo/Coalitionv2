using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPart : MonoBehaviour {

    public Sprite[] Sprites;
    public float runningSpeed;

    [SerializeField]
    private bool flipSymmetrically = false;
    private float runningOutPosition;
    private SpriteRenderer spriteRenderer;
    private float tileSize;
    //(float)tileSet[0].renderer.bounds.size.x;

    public Events.OnBackGroundRan OnPositionReached;

    private void Update()
    {
        transform.position -= new Vector3(0, runningSpeed);
        if (transform.position.y <= -(tileSize - 0.2f))
            changePosition();
    }

    void Start ()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        tileSize = spriteRenderer.size.y;
	}

    public void changePosition()
    {
        int i = Random.Range(0, Sprites.Length);
        spriteRenderer.sprite = Sprites[i];
        if (flipSymmetrically)
        {
            i = Random.Range(0, 4);
            switch (i)
            {
                case 0:
                    spriteRenderer.flipX = true;
                    break;
                case 1:
                    spriteRenderer.flipX = false;
                    break;
                case 2:
                    spriteRenderer.flipY = true;
                    break;
                case 3:
                    spriteRenderer.flipY = false;
                    break;
            }
        }
        OnPositionReached.Invoke(transform, tileSize);
    }


    public void SetRunningOutPos(float y)
    {
        runningOutPosition = y;
    }
}
