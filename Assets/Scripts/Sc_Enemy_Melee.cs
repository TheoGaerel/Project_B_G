using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Enemy_Melee : Sc_Enemy
{
    [SerializeField]
    private float F_ATTACK_WARMUP = 0.75f;
    [SerializeField]
    private float F_ATTACK_RADIUS = 2.35f;
    [SerializeField]
    private GameObject go_warningMeleeArea;
    [SerializeField]
    private Transform meleeCenterPoint;

    private bool b_meleeAttack = false;
    //20% faster than player
    //cone attack with delay 


    protected override void Behavior()
    {
        if (b_meleeAttack) return;


        var targetRotation = Quaternion.LookRotation(Sc_PlayerController.Instance.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10 * Time.deltaTime);
        if (Vector3.Distance(this.transform.position, Sc_PlayerController.Instance.transform.position) > F_ATTACK_RADIUS)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(Sc_PlayerController.Instance.transform.position);
        }
        else
        {
            navMeshAgent.isStopped = true;
        }

        if (f_attackdelay > 0f) f_attackdelay -= Time.deltaTime;
        if (f_attackdelay <= 0f && navMeshAgent.isStopped)
        {
            b_meleeAttack = true;
            StartCoroutine(RoutineMeleeAttack());
        }
    }

    private IEnumerator RoutineMeleeAttack()
    {
        navMeshAgent.isStopped = true;
        if (go_warningMeleeArea) go_warningMeleeArea.gameObject.SetActive(true);

        //attack swipe
        yield return new WaitForSeconds(F_ATTACK_WARMUP);

        go_warningMeleeArea.GetComponent<SpriteRenderer>().color = Color.red;
        Vector3 dirFromAtoB = (Sc_PlayerController.Instance.transform.position - this.transform.position).normalized;
        float dotProd = Vector3.Dot(dirFromAtoB, this.transform.forward);

        if (dotProd > 0) //the player is in front of this object
        {
            Collider[] cols = Physics.OverlapSphere(meleeCenterPoint.transform.position, F_ATTACK_RADIUS);

            foreach (Collider col in cols)
            {
                if (col.CompareTag("Player"))
                {
                    col.GetComponent<Sc_Player>().OnDamage();
                    break;
                }
            }
        }

        yield return new WaitForSecondsRealtime(0.2f);
        go_warningMeleeArea.GetComponent<SpriteRenderer>().color = Color.white;
        if (go_warningMeleeArea) go_warningMeleeArea.gameObject.SetActive(false);
        f_attackdelay = F_MAX_ATTACK_DELAY;
        b_meleeAttack = false;
    }

    private void OnDrawGizmos()
    {
        if (meleeCenterPoint == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(meleeCenterPoint.transform.position, F_ATTACK_RADIUS);
    }
}
