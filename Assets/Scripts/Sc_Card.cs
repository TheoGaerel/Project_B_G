using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Sc_Card : MonoBehaviour
{
    [SerializeField]
    protected Sc_Projectile.Reload reload;
    [SerializeField]
    private Button button;
    public abstract void OnUse();
    protected void MoveToEndOfStack()
    {
        this.transform.SetAsFirstSibling();
        this.gameObject.SetActive(false);
    }

    protected void ShootProjectile(float angle, Sc_Projectile.Recoil recoil, Sc_Projectile.Range range, float overrideSize, float overrideSpeed, Sc_Projectile.ProjectileType projectileType)
    {
        Sc_Projectile projectile = Sc_PoolProjectiles.Instance.RequestProjectile();
        projectile.Init(Sc_Projectile.Team.Player, Sc_PlayerController.Instance.gameObject, range, overrideSpeed, projectileType);
        projectile.gameObject.SetActive(true);
        projectile.transform.position = Sc_PlayerController.Instance.trsf_launchPosition.position;
        projectile.transform.rotation = Sc_PlayerController.Instance.transform.rotation;
        projectile.transform.localScale = Vector3.one * overrideSize;
        projectile.transform.Rotate(0, angle, 0);
        Sc_PlayerController.Instance.OnRecoil(recoil);
    }

    public void ShowButton(bool show)
    {
        if (button) button.gameObject.SetActive(show);
    }

    public void OnClickButtonAltar()
    {
        Sc_PanelAltar.Instance.OnChoiceCard(this);
    }
}
