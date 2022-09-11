using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Damager
{
    public override void HitSomething()
    {
       ObjectsPool.Instance.DespawnToPool(gameObject, 0);
    }
}
