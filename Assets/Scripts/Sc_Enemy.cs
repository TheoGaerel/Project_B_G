using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Sc_Enemy : Sc_Entity
{
    [SerializeField]
    protected float f_moveSpeed = 5f;
    protected float F_MAX_ATTACK_DELAY = 4f;
    protected float f_attackdelay;

    protected NavMeshAgent navMeshAgent;

    protected virtual void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        f_attackdelay = F_MAX_ATTACK_DELAY;
        f_attackdelay += Random.Range(-1f, 1f);
    }
    public override void OnDamage()
    {
        f_lifeAmount--;
        if (f_lifeAmount <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        Behavior();
    }

    protected abstract void Behavior();

}
