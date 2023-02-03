using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Card_Projectile_Shotgun : Sc_Card_Projectile
{
    public override void OnUse()
    {
        MoveToEndOfStack();
        Debug.Log("Shotgun Shoot");
        ShootProjectile(-30);
        ShootProjectile(0);
        ShootProjectile(30);
        StartCoroutine(RoutineToNextCard());
    }
}
