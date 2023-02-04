using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Card_Projectile_Sniper : Sc_Card_Projectile
{
    public override void OnUse()
    {
        ShootProjectile(0, recoil, range, 1f, 4, Sc_Projectile.ProjectileType.Sniper);

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

    private IEnumerator RoutineBoost()
    {
        yield return new WaitForSeconds(0.25f);
        ShootProjectile(0, recoil, range, 1f, 4, Sc_Projectile.ProjectileType.Sniper);
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
