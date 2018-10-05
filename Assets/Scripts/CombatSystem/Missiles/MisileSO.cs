using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon/Misile", order = 1)]
public class MisileSO : ScriptableObject
{
    public GameObject misile;
    public float coolDown;
    public AudioClip[] shooting;
   
}
