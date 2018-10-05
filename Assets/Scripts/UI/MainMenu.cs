using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Button startGameButton;
    public InputField worldSizeInput;
    public InputField numberOfPiratesInput;

    [SerializeField]
    private Animation mainMenuAnimator;
    [SerializeField]
    private AnimationClip fadeOutAnimation;
    [SerializeField]
    private AnimationClip fadeInAnimation;

    public Events.EventFadeComplete OnMainMenuFadeComplete;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        startGameButton.onClick.AddListener(StartGame);

        if (mainMenuAnimator == null)
            mainMenuAnimator = gameObject.GetComponent<Animation>();

    }


    public void OnFadeOutComplete() // Animation Event
    {
        OnMainMenuFadeComplete.Invoke(true);
        SetMenuActive(false);
    }

    public void OnFadeInComplete() // Animation Event
    {
        OnMainMenuFadeComplete.Invoke(false);
        UIManager.Instance.SetDummyCamreActive(true);
        SetMenuActive(true);
    }

    public void FadeIn() 
    {
        mainMenuAnimator.Stop();
        mainMenuAnimator.clip = fadeInAnimation;
        mainMenuAnimator.Play();
    }

    public void FadeOut()
    {
        UIManager.Instance.SetDummyCamreActive(false);
        mainMenuAnimator.Stop();
        mainMenuAnimator.clip = fadeOutAnimation;
        mainMenuAnimator.Play();
    }

    private void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        if (previousState == GameManager.GameState.PREGAME && currentState == GameManager.GameState.RUNNING)
        {
            FadeOut();
        }

        if (previousState != GameManager.GameState.PREGAME && currentState == GameManager.GameState.PREGAME)
        {
            FadeIn();
        }

    }

    private void StartGame()
    {
        int size;
        int numPirates;

        if (int.TryParse(worldSizeInput.text, out size))
        {
            Debug.Log("Size is: " + size);
        }
        else
        {
            worldSizeInput.text = "Try numbers";
        }

        if (int.TryParse(numberOfPiratesInput.text, out numPirates))
        {
            Debug.Log("There is: " + numPirates);
        }
        else
        {
            numberOfPiratesInput.text = "Try numbers";
            return;
        }

        WorldStateSO worldState = WorldStateSO.CreateInstance<WorldStateSO>();
        worldState.Initialize(size, size, numPirates);

        GameManager.Instance.StartGame(worldState);
    }

    private void SetMenuActive(bool active)
    {
        Transform[] transforms = gameObject.GetComponentsInChildren<Transform>(active);
        List<Transform> listedTransforms = new List<Transform>(transforms);

        listedTransforms.Remove(gameObject.transform);
        listedTransforms.Remove(GameObject.Find("Background").transform);

        foreach (Transform tr in listedTransforms)
            tr.gameObject.SetActive(active);
    }
}
