using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Card_Projectile_Shotgun : Sc_Card_Projectile
{
    public override void OnUse()
    {

        if (Sc_Player.Instance.i_boostAmount > 1)
        {
            Sc_Player.Instance.SetBoostAmount(Sc_Player.Instance.i_boostAmount - 1);
            StartCoroutine(RoutineBoost());
        }
        else
        {
            ShootProjectile(-15, recoil, range, 0.5f, 1, Sc_Projectile.ProjectileType.Shotgun);
            ShootProjectile(0, recoil, range, 0.5f, 1, Sc_Projectile.ProjectileType.Shotgun);
            ShootProjectile(15, recoil, range, 0.5f, 1, Sc_Projectile.ProjectileType.Shotgun);
            MoveToEndOfStack();
            Sc_PlayerController.Instance.StartDelayNextCard(reload);
        }
    }

    private IEnumerator RoutineBoost()
    {
        yield return new WaitForSeconds(0.25f);
        ShootProjectile(-15, recoil, range, 0.5f, 1, Sc_Projectile.ProjectileType.Shotgun);
        ShootProjectile(0, recoil, range, 0.5f, 1, Sc_Projectile.ProjectileType.Shotgun);
        ShootProjectile(15, recoil, range, 0.5f, 1, Sc_Projectile.ProjectileType.Shotgun);
        if (Sc_Player.Instance.i_boostAmount > 1)
        {
            Sc_Player.Instance.SetBoostAmount(Sc_Player.Instance.i_boostAmount - 1);
            StartCoroutine(RoutineBoost());
        }
        else
        {
            MoveToEndOfStack();
            Sc_PlayerController.Instance.StartDelayNextCard(reload);
        }
    }
}
