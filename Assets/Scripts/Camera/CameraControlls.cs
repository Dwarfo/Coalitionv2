using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlls : MonoBehaviour {

    //Clamp values
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
    [SerializeField]
    private Transform player;

    // Use this for initialization
    void Start ()
    {
        WorldGenerator.Instance.onPlayerReady.AddListener(HandleOnPlayerReady);
        computeClamps();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 newPosition = new Vector3(Mathf.Clamp(player.position.x, minX, maxX), Mathf.Clamp(player.position.y, minY, maxY), transform.position.z);
        transform.position = newPosition;
    }


    private void computeClamps()
    {
        Camera thisCamera = gameObject.GetComponent<Camera>();
        //Getting Clamp values depending on camera Size and AspectRatio
        minX = thisCamera.orthographicSize * thisCamera.aspect;
        minY = thisCamera.orthographicSize;
        //maxX = Mathematical.worldSize * 2.5f - minX;
        //maxY = Mathematical.worldSize * 2.5f - minY;
        if (GameManager.Instance != null)
        {
            maxX = GameManager.Instance.worldState.x * WorldGenerator.Instance.Offset - minX;
            maxY = GameManager.Instance.worldState.y * WorldGenerator.Instance.Offset - minY;
        }
        else
        {
            maxX = WorldGenerator.Instance.testWorld.x * WorldGenerator.Instance.Offset - minX;
            maxY = WorldGenerator.Instance.testWorld.y * WorldGenerator.Instance.Offset - minY;
        }

    }

    private void HandleOnPlayerReady(GameObject player)
    {
        this.player = player.GetComponent<Transform>();
    }
}
