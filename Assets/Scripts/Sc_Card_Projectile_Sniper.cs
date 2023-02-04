using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Card_Projectile_Sniper: Sc_Card_Projectile
{
    public override void OnUse()
    {
        MoveToEndOfStack();
        ShootProjectile(0, recoil, range,1f,4,Sc_Projectile.ProjectileType.Sniper);
        Sc_PlayerController.Instance.StartDelayNextCard(reload);
    }
}
