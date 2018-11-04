using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GoUp_Move : Move
{
    public Transform tr;
    public float speed = 0.1f;
    private float time = 0;


    public override void makeAction(float deltaTime)
    {
        tr.position += new Vector3(0, speed);
        time += deltaTime;
        if (time > duration && onActionFinished != null)
            onActionFinished.Invoke(this);
    }

    public override void undoAction()
    {

    }

    public override void Initialize(GameObject go)
    {
        tr = go.GetComponent<Transform>();
        Debug.Log("Initialized");
    }

    public override float GetDuration()
    {
        return duration;
    }


}
