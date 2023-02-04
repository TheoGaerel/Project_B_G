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

    private bool b_ready = false;
    protected NavMeshAgent navMeshAgent;
    protected Sc_CanvasEnemy canvasEnemy;
    protected virtual void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        f_attackdelay = F_MAX_ATTACK_DELAY;
        f_attackdelay += Random.Range(-1f, 1f);
        canvasEnemy = GetComponentInChildren<Sc_CanvasEnemy>();
        for (int i = 0; i < canvasEnemy.list_hearts.Count; i++)
        {
            if (i < f_lifeAmount) canvasEnemy.list_hearts[i].gameObject.SetActive(true);
            else canvasEnemy.list_hearts[i].gameObject.SetActive(false);
        }
        StartCoroutine(RoutineDelayStart());
    }
    public override void OnDamage()
    {
        if (!b_ready) return;
        f_lifeAmount--;

        for (int i = 0; i < canvasEnemy.list_hearts.Count; i++)
        {
            if (i < f_lifeAmount) canvasEnemy.list_hearts[i].gameObject.SetActive(true);
            else canvasEnemy.list_hearts[i].gameObject.SetActive(false);
        }

        if (f_lifeAmount <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!b_ready) return;
        Behavior();
    }

    private IEnumerator RoutineDelayStart()
    {
        yield return new WaitForSeconds(2f);
        b_ready = true;
    }

    protected abstract void Behavior();

}
