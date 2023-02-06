using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Enemy_Ranged : Sc_Enemy
{
    private const float F_DISTANCE_TO_PLAYER = 15f;
    [SerializeField]
    private Transform trsf_launchPoint;
    private bool b_distanceAttack = false;

    protected override void Behavior()
    {
        this.transform.LookAt(new Vector3(Sc_PlayerController.Instance.transform.position.x, this.transform.position.y, Sc_PlayerController.Instance.transform.position.z));
        if (b_distanceAttack) return;
        if (Vector3.Distance(this.transform.position, Sc_PlayerController.Instance.transform.position) > F_DISTANCE_TO_PLAYER)
        {
            animator.SetBool("b_Run", true);
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(Sc_PlayerController.Instance.transform.position);

        }
        else
        {
            animator.SetBool("b_Run", false);
            navMeshAgent.isStopped = true;
        }

        if (f_attackdelay > 0f) f_attackdelay -= Time.deltaTime;
        if (f_attackdelay <= 0f)
        {
            f_attackdelay = F_MAX_ATTACK_DELAY;
            b_distanceAttack = true;
            StartCoroutine(ShootProjectile());
        }
    }

    protected IEnumerator ShootProjectile()
    {
        yield return new WaitForSeconds(0.8f);
        Sc_Projectile projectile = Sc_PoolProjectiles.Instance.RequestProjectile();
        projectile.Init(Sc_Projectile.Team.Enemies, this.gameObject, Sc_Projectile.Range.Far_100, 1, Sc_Projectile.ProjectileType.Enemy);
        projectile.gameObject.SetActive(true);
        projectile.transform.localScale = Vector3.one * 0.5f;
        projectile.transform.position = trsf_launchPoint.position;
        projectile.transform.rotation = this.transform.rotation;
        animator.SetTrigger("t_Attack");
        yield return new WaitForSeconds(0.4f);
        b_distanceAttack = false;
    }
}
