using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EnemyBSHM : MonoBehaviour {

    [SerializeField]
    private List<Move> actions;
    private MakeMove action;
    private int actionNum = 0;

    public Events.FloatEvent OnEnemyDeath;

	// Use this for initialization
	void Start ()
    {
        actions = transform.GetComponentInChildren<ActionList>().actions;
        foreach (var action in actions)
        {
            action.Initialize(gameObject);
            action.onActionFinished.AddListener(HandleOnActionFinished);
        }

        action += actions[actionNum].makeAction;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(action != null)
            action(Time.deltaTime);
	}

    private void HandleOnActionFinished(Move thatMove)
    {
        Debug.Log("Action finished");

        if(!thatMove.dontUnsub)
            action -= thatMove.makeAction;

        if (actionNum < actions.Count - 1)
        {
            actionNum++;
            action += actions[actionNum].makeAction;
        }


    }


}

public delegate void MakeMove(float deltaTime);
