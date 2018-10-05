using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PirateStates : MonoBehaviour
{

    private float topSpeed;
    private Vector2 direction;
    private Transform target;
    private Rigidbody2D pirate;
    private Action doAction;
    private float dragValue;
    private Weapon weapon;
    private IEnumerator coroutine;
    private WeightedRandom<randomAction> rand;
    private Transform firePos;


    void Start()
    {
        dragValue = gameObject.GetComponent<Stats>().drag;
        gameObject.GetComponent<PirateBehaviour>().onBehaviourChanged.AddListener(HandleOnBehaviourChanged);
        pirate = gameObject.GetComponent<Rigidbody2D>();
        topSpeed = gameObject.GetComponent<Stats>().speed;
        doAction = null;
        weapon = gameObject.GetComponent<Weapon>();

        if (firePos == null)
            firePos = gameObject.transform.Find("FirePosition");


        rand = new WeightedRandom<randomAction>();
        rand.AddItem(new randomAction(10, 2, Rotate));
        rand.AddItem(new randomAction(100, 0, Idling));
        rand.AddItem(new randomAction(5, 0.3f, Fire));

        /*
        #region Testing Random

        WeightedRandom<Actioneer> Wr = new WeightedRandom<Actioneer>();
        Wr.AddItem(new Actioneer("first", 5));
        Wr.AddItem(new Actioneer("2nd", 1));
        Wr.AddItem(new Actioneer("3nd", 33));
        Wr.AddItem(new Actioneer("4nd", 2));
        Wr.AddItem(new Actioneer("5th", 3));
        Wr.AddItem(new Actioneer("6th", 7));
        Wr.AddItem(new Actioneer("7th", 6));
        int i = 0;

        for (int j = 0; j < 100; j++)
        {
            Debug.Log(" Random Weight" + Wr.GetRandomValue(out i).name);
        }

        /*foreach (var items in Wr.allWeights())
            Debug.Log("Random Weight " + items.Weight + " " + items.name);
        

        #endregion
        */
    }

    // Update is called once per frame
    void Update ()
    {
        if (target != null)
        {
            direction = (target.position - transform.position).normalized;
        }
        if(doAction != null)
            doAction();
    }

    private void HandleOnBehaviourChanged(Behaviours newBehaviour)
    {
        target = gameObject.GetComponent<PirateBehaviour>().GetTarget();
        switch (newBehaviour)
        {
            case Behaviours.ATTACKING:
                gameObject.GetComponent<Rigidbody2D>().drag = dragValue;
                doAction = AttackTarget;
                
                
                break;
            case Behaviours.CHASING:
                gameObject.GetComponent<Rigidbody2D>().drag = 0;
                doAction = ChaseTarget;
                Debug.Log("I see Player");
                break;
            case Behaviours.FLEEING:

                break;
            case Behaviours.PATROLING:
                gameObject.GetComponent<Rigidbody2D>().drag = dragValue;
                doAction = null;
                break;

        }

    }


    private void ChaseTarget()
    {
        //float distance = Vector2.Distance(gameObject.transform.position, target.position);
        pirate.AddForce(direction * 5);
        pirate.velocity = ClampVelocity(pirate.velocity);
    }


    private void AttackTarget()
    {
        randomAction action = rand.GetRandomValue();

        if (action.isDone)
        {
            doAction += action.toDoAction;
            StartCoroutine(RemoveAction(action));
        }

        Debug.Log("Number of listeners: " + doAction.GetInvocationList().Length);

    }

    private void Rotate()
    {
        transform.RotateAround(target.position, transform.forward, 1);

    }

    private void Fire()
    {
        weapon.FireProjectile(firePos);
    }

    private void Idling()
    {

    }

    private IEnumerator RemoveAction(randomAction actionToRemove)
    {
        actionToRemove.isDone = false;
        yield return new WaitForSeconds(actionToRemove.waitFor);
        doAction -= actionToRemove.toDoAction;
        actionToRemove.isDone = true;

    }

    private Vector2 ClampVelocity(Vector2 velocity)
    {
        float x = Mathf.Clamp(velocity.x, -topSpeed, topSpeed);
        float y = Mathf.Clamp(velocity.y, -topSpeed, topSpeed);

        return new Vector2(x, y);
    }





}

public delegate void DoAction();

public class randomAction : IWeighted
{
    private string actionName;
    private int weight;
    public Action toDoAction;
    public float waitFor;

    public bool isDone = true;
    private IEnumerator coroutine;


    public int Weight
    {
        get
        {
            return weight;
        }
        set
        {
            if (weight < 0)
                weight = -value;
            else
                weight = value;
        }
    }

    public randomAction(int weight, float waitFor, Action action)
    {
        this.waitFor = waitFor;
        this.weight = weight;
        toDoAction = action;
    }


    public override string ToString()
    {
        return "Action: " + actionName ;
    }
}