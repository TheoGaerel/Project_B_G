using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Card_Projectile : Sc_Card
{
    public override void OnUse()
    {
        MoveToEndOfStack();
        Debug.Log("Normal Shoot");
        ShootProjectile(0);
        StartCoroutine(RoutineToNextCard());
    }
}
