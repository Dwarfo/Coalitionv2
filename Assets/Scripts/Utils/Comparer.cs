using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comparer : IComparer<Collider2D>
{

    private Transform compareTo;

    public Comparer(Transform tr)
    {
        compareTo = tr;
    }

    public int Compare(Collider2D x, Collider2D y)
    {
        Vector2 distanceX = x.transform.position - compareTo.position;
        float xDistance = distanceX.sqrMagnitude;

        Vector2 distanceY = y.transform.position - compareTo.position;
        float yDistance = distanceY.sqrMagnitude;

        return xDistance.CompareTo(yDistance);
    }
}
