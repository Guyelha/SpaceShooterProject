using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damager : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    public int GetDamage()
    {
        return damage;
    }

    public virtual void HitSomething()
    {
        Debug.Log("hit something");
    }

}
