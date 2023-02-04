using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Card_Projectile_Shotgun : Sc_Card_Projectile
{
    public override void OnUse()
    {
        MoveToEndOfStack();
        ShootProjectile(-15, recoil, range, 0.5f, 1, Sc_Projectile.ProjectileType.Shotgun);
        ShootProjectile(0, recoil, range, 0.5f, 1, Sc_Projectile.ProjectileType.Shotgun);
        ShootProjectile(15, recoil, range, 0.5f, 1, Sc_Projectile.ProjectileType.Shotgun);
        Sc_PlayerController.Instance.StartDelayNextCard(reload);
    }
}
