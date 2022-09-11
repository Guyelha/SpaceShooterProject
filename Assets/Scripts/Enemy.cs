using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Damager
{
    public override void HitSomething()
    {
        Destroy(gameObject);
    }
}
