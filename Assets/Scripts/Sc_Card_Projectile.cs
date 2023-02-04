using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Card_Projectile : Sc_Card
{
    [SerializeField] protected Sc_Projectile.Range range;

    [SerializeField] protected Sc_Projectile.Recoil recoil;
    public override void OnUse()
    {
        ShootProjectile(0, recoil, range, 0.5f, 1f, Sc_Projectile.ProjectileType.Revolver);

        if (Sc_Player.Instance.i_boostAmount > 1)
        {
            Sc_Player.Instance.SetBoostAmount(Sc_Player.Instance.i_boostAmount - 1);
            StartCoroutine(RoutineBoost());
        }
        else
        {
            Sc_PlayerController.Instance.StartDelayNextCard(reload);
            MoveToEndOfStack();
        }
     
    }

    private IEnumerator RoutineBoost()
    {
        yield return new WaitForSeconds(0.25f);
        ShootProjectile(0, recoil, range, 0.5f, 1f, Sc_Projectile.ProjectileType.Revolver);
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
