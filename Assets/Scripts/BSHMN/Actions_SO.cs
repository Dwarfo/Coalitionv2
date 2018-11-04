using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewActionPack", menuName = "AI/PirateActionPack", order = 1)]
public class Actions_SO : ScriptableObject
{
    private List<Move> actions = new List<Move>();
    private Move finalAction;



}


public abstract class Move : MonoBehaviour
{
    public bool dontUnsub = false;
    public Events.OnActionFinished onActionFinished;
    public float duration = 2f;

    public abstract void makeAction(float deltaTime);
    public abstract void  undoAction();
    public abstract void  Initialize(GameObject go);
    public abstract float GetDuration();

}


