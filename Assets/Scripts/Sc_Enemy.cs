using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Sc_Enemy : Sc_Entity
{
    [SerializeField]
    protected Animator animator;
    protected float f_moveSpeed = 5f;
    [SerializeField]
    protected float F_MAX_ATTACK_DELAY = 4f;
    protected float f_attackdelay;

    private bool b_ready = false;
    protected NavMeshAgent navMeshAgent;
    protected Sc_CanvasEnemy canvasEnemy;

    [SerializeField]
    private AudioSource sound_OnHit;
    protected virtual void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
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
        if (!b_ready || !Sc_PlayerController.Instance.b_canInteract) return;
        f_lifeAmount--;

        if (sound_OnHit) sound_OnHit.Play();
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
        if (!b_ready || !Sc_PlayerController.Instance.b_canInteract)
        {
            this.transform.LookAt(new Vector3(Sc_PlayerController.Instance.transform.position.x, this.transform.position.y, Sc_PlayerController.Instance.transform.position.z));
            navMeshAgent.isStopped = true;
            return;
        }
        Behavior();
    }

    private IEnumerator RoutineDelayStart()
    {
        yield return new WaitForSeconds(2f);
        b_ready = true;
    }

    protected abstract void Behavior();

}
