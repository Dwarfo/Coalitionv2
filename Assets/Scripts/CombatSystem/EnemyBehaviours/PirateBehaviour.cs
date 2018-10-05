using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateBehaviour : MonoBehaviour {
    
    public Behaviours currentBehaviour;


    [SerializeField]
    private Transform target;
    [SerializeField]
    private float checkRadius;
    [SerializeField]
    private LayerMask checkingLayers;
    private List<Collider2D> characters;
    
    private float distance;

    public Events.OnBehaviourChanged onBehaviourChanged;


    private void Start()
    {
        characters = new List<Collider2D>();

        transform.GetChild(0).GetComponent<CircleCollider2D>().radius = checkRadius;
        currentBehaviour = Behaviours.PATROLING;
        
    }

    private void Update()
    {
        if (target != null)
        {
            distance = Vector2.Distance(transform.position, target.position);
            if (distance < 8 && currentBehaviour != Behaviours.ATTACKING)
                ChangeBehaviour(Behaviours.ATTACKING);
            else if (distance > 8)
                ChangeBehaviour(Behaviours.CHASING);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("I see ");
        Transform target = collision.GetComponent<Transform>();
        if (collision.gameObject.GetComponent<Stats>() != null && collision.gameObject.GetComponent<Stats>()._Faction == Faction.PLAYER)
        {
            this.target = target;
            ChangeBehaviour(Behaviours.CHASING);
            //audioSource.Play();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Transform target = collision.GetComponent<Transform>();
        if (collision.gameObject.GetComponent<Stats>() != null && collision.gameObject.GetComponent<Stats>()._Faction == Faction.PLAYER)
        {
            this.target = null;
            ChangeBehaviour(Behaviours.PATROLING);
            //audioSource.Stop();
            //audioSource.volume = 0.05f;
            //audioSource.PlayOneShot(running);
        }

    }

    private void ChangeBehaviour(Behaviours newBehaviour)
    {
        currentBehaviour = newBehaviour;
        onBehaviourChanged.Invoke(currentBehaviour);
    }


    public Transform GetTarget()
    {
        return target;
    }

}

public enum Behaviours
{
    PATROLING,
    CHASING,
    ATTACKING,
    FLEEING
}