using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Card_Projectile : Sc_Card
{
    [SerializeField] protected Sc_Projectile.Range range;

    [SerializeField] protected Sc_Projectile.Recoil recoil;
    public override void OnUse()
    {
        MoveToEndOfStack();
        ShootProjectile(0, recoil, range,0.5f,1f, Sc_Projectile.ProjectileType.Revolver);
        Sc_PlayerController.Instance.StartDelayNextCard(reload);
    }
}
