using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton_MD<UIManager> {

    public MainMenu mainMenu;
    [SerializeField]
    private Camera dummyCamera;
    [SerializeField]
    private PauseMenu pauseMenu;
    [SerializeField]
    private GameObject miniMap;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        GameManager.Instance.OnGameStateChanged.AddListener(HandleStartGame);
    }

    private void Update()
    {
        
    }

    public void SetDummyCamreActive(bool active)
    {
        dummyCamera.gameObject.SetActive(active);
    }

    private void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        pauseMenu.gameObject.SetActive(currentState == GameManager.GameState.PAUSED);
        miniMap.SetActive(currentState != GameManager.GameState.PAUSED);
    }

    private void HandleStartGame(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        if (previousState == GameManager.GameState.PREGAME && currentState == GameManager.GameState.RUNNING)
        {
            miniMap.SetActive(true);
        }
        GameManager.Instance.OnGameStateChanged.RemoveListener(HandleStartGame);
    }
}
