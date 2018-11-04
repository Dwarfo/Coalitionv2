using UnityEngine.Events;
using UnityEngine;

public class Events
{
    [System.Serializable]
    public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState> { }

    [System.Serializable]
    public class EventFadeComplete : UnityEvent<bool> { }

    [System.Serializable]
    public class OnDestroy : UnityEvent { }

    [System.Serializable]
    public class OnPlayerReady : UnityEvent<GameObject> { }

    [System.Serializable]
    public class OnBehaviourChanged : UnityEvent<Behaviours> { }

    [System.Serializable]
    public class OnBattleStateChanged : UnityEvent<BattleState> { }

    [System.Serializable]
    public class OnPlayerDamageReceived : UnityEvent<float> { }

    [System.Serializable]
    public class OnCoolDownStarted : UnityEvent<float> { }

    [System.Serializable]
    public class OnBackGroundRan : UnityEvent<Transform, float> { }

    [System.Serializable]
    public class OnActionFinished : UnityEvent<Move> { }

    //[System.Serializable]
    public class FloatEvent : UnityEvent<float> { }
}
