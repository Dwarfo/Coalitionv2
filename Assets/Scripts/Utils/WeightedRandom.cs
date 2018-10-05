using System.Collections;
using System.Collections.Generic;
using System;


public class WeightedRandom<T> where T : IWeighted/*, IComparable<T>*/
{
    private int weightSum = 0;
    private List<T> items = new List<T>();
    private Dictionary<T,int> defaultWeights = new Dictionary<T, int>();
    private bool weightsPut = false;

    private Random rnd = new Random();

    public WeightedRandom()
    {
   
    }

    public WeightedRandom(List<T> items)
    {
        this.items = new List<T>(items);
        foreach (var item in items)
        {
            defaultWeights.Add(item, item.Weight);
            weightSum += item.Weight;
        }

        SetCummulativeWeights();
    }

    public void AddItem(T newItem)
    {
        weightSum += newItem.Weight;
        items.Add(newItem);
        defaultWeights.Add(newItem, newItem.Weight);
    }

    public T GetRandomValue()
    {
        if (!weightsPut)
        {
            SetCummulativeWeights();
        }
        
        int value = rnd.Next(weightSum);
        foreach (var item in items)
        {
            if (value <= item.Weight)
                return item;
        }
        return default(T);
    }

    private void SetCummulativeWeights()
    {
        items.Sort((x, y) => -x.Weight.CompareTo(y.Weight));
        int acummulation = 0;
        weightsPut = true;

        foreach (var item in items)
        {
            acummulation += item.Weight;
            item.Weight = acummulation;
        }
    }

    public List<T> allWeights()
    {
        return items;
    }
}
