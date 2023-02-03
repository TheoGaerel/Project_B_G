using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sc_Card : MonoBehaviour
{
    public abstract void OnUse();
    protected void MoveToEndOfStack()
    {
        this.transform.SetAsFirstSibling();
    }

    protected IEnumerator RoutineToNextCard()
    {
        yield return new WaitForSeconds(0.5f);
        Sc_PlayerController.Instance.SetShotLock(false);
    }

    protected void ShootProjectile(float angle)
    {
        Sc_Projectile projectile = Sc_PoolProjectiles.Instance.RequestProjectile();
        projectile.Init(Sc_Projectile.Team.Player, this.gameObject);
        projectile.gameObject.SetActive(true);
        projectile.transform.position = Sc_PlayerController.Instance.trsf_launchPosition.position;
        projectile.transform.rotation = Sc_PlayerController.Instance.transform.rotation;

        projectile.transform.Rotate(0, angle, 0);
    }
}
