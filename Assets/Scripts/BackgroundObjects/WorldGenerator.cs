using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : Singleton_MD<WorldGenerator> {

    public GameObject BigMeteor;
    public GameObject Player;
    public WorldStateSO testWorld;
    public Events.OnPlayerReady onPlayerReady;

    [SerializeField]
    private Sprite background;
    [SerializeField]
    private Sprite[] smallMeteorSprites;
    [SerializeField]
    private Sprite[] bigMeteorSprites;

    private int width;
    private int heigth;
    private float offset = 2.5f;

    public float Offset
    {
        get { return offset; }
    }

	void Start ()
    {
        if (GameManager.Instance != null)
        {
            width = GameManager.Instance.worldState.x;
            heigth = GameManager.Instance.worldState.y;
        }
        else
        {
            width = testWorld.x;
            heigth = testWorld.y;
        }
        GenerateWorld();
        SpawnPlayer();
        //Debug.Log(width + " : width, " + heigth + " : height");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        if (previousState == GameManager.GameState.PREGAME && currentState == GameManager.GameState.RUNNING)
        {
            GenerateWorld();
            Debug.Log("World Generated");
        }
    }

    private void GenerateWorld()
    {
        

        int k = 0;

        for (int i = 0; i <= width; i++)
        {
            for (int j = 0; j <= heigth; j++)
            {
                CreateBackgroundTile(i, j, k);
                PopulateWithObjects(i, j, k);
                k++;
            }
        }

        CreateBounds(width, heigth);
    }

    private void CreateBackgroundTile(int i, int j, int k)
    {
        //Creating a tile gameObject and placing on some coordinates
        GameObject go = new GameObject();

        go.name = "Background" + (k).ToString();
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        go.transform.position = new Vector2(i * offset, j * offset);
        renderer.sprite = background;
        //Make tile a child of a BackGround creator and putting in Background sorting layer
        go.transform.SetParent(transform, false);
        renderer.sortingLayerName = "Background";

    }

    private void CreateBounds(int width, int heigth)
    {
        //getting a gameObject that represents Bounds
        GameObject bounds = transform.Find("Bounds").gameObject;

        bounds.transform.position = new Vector2((width * offset) / 2, (heigth * offset) / 2);

        GameObject[] boundaries = new GameObject[4];
        BoxCollider2D[] bColliders = new BoxCollider2D[4];
        // 4 gameObject's, all having it's own colider, on each side of the map
        for (int i = 0; i < 4; i++)
        {
            boundaries[i] = new GameObject();
            boundaries[i].transform.SetParent(bounds.transform, false);
            boundaries[i].name = "Boundary" + i.ToString();

            bColliders[i] = boundaries[i].AddComponent<BoxCollider2D>();
            boundaries[i].layer = 9;
            //bColliders[i].usedByComposite = true;
        }

        //Placing bound gameObject's on the sides of the map
        boundaries[0].transform.position = new Vector2(0, (heigth * offset) / 2);
        boundaries[1].transform.position = new Vector2((width * offset) / 2, heigth * offset);
        boundaries[2].transform.position = new Vector2(width * offset, (heigth * offset) / 2);
        boundaries[3].transform.position = new Vector2((width * offset) / 2, 0);
        //Adjusting collider sizes according to the map size
        bColliders[0].size = new Vector2(1, heigth * offset);
        bColliders[1].size = new Vector2(width * offset, 1);
        bColliders[2].size = new Vector2(1, heigth * offset);
        bColliders[3].size = new Vector2(width * offset, 1);
    }

    private void PopulateWithObjects(int i, int j, int k)
    {
        
        //Randomly generate objects taken from Resources
        int meteor = Random.Range(1, 10);

        if (j != heigth && j != 0)
        {
            if (meteor >= 9)
            {
                GameObject mGo = Instantiate(BigMeteor, new Vector2(i * offset + Random.Range(0, 2), j * offset + Random.Range(0, 2)), Quaternion.Euler(transform.rotation.x, transform.rotation.y, Random.Range(0, 180)));
                SpriteRenderer mRenderer = mGo.GetComponent<SpriteRenderer>();
                mRenderer.sprite = bigMeteorSprites[Random.Range(0, bigMeteorSprites.Length - 1)];
                mRenderer.sortingLayerName = "BackgroundObjects";
                mGo.transform.SetParent(this.transform, false);

            }
        }

    }

    private void SpawnPlayer()
    {
        GameObject player = Instantiate(Player, new Vector2(3 * offset + Random.Range(0, 2), 3 * offset + Random.Range(0, 2)), Quaternion.identity);
        onPlayerReady.Invoke(player);
    }
}
