using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sc_Player : Sc_Entity
{
    private static Sc_Player sc_instance;

    public static Sc_Player Instance
    {
        get
        {
            if (sc_instance == null)
            {
                sc_instance = FindObjectOfType<Sc_Player>();
            }
            return sc_instance;
        }
    }

    [SerializeField]
    private List<Image> list_Hearts = new List<Image>();
    public int i_boostAmount { get; private set; } = 1;
    private bool b_Invincible = false;


    [SerializeField]
    private AudioSource sound_OnHit;


    private void Start()
    {
        for (int i = 0; i < list_Hearts.Count; i++)
        {
            if (i < f_lifeAmount) list_Hearts[i].gameObject.SetActive(true);
            else list_Hearts[i].gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnDamage();
    }

    public override void OnDamage()
    {
        if (!Sc_PlayerController.Instance.b_canInteract || b_Invincible) return;
        f_lifeAmount--;
        if (sound_OnHit) sound_OnHit.Play();
        for (int i = 0; i < list_Hearts.Count; i++)
        {
            if (i < f_lifeAmount) list_Hearts[i].gameObject.SetActive(true);
            else list_Hearts[i].gameObject.SetActive(false);
        }

        if (f_lifeAmount <= 0)
        {
            Sc_PlayerController.Instance.SetCanInteract(false);
            Sc_PlayerController.Instance.animator.SetTrigger("t_Death");
            StartCoroutine(RoutineDeath());
        }

        b_Invincible = true;
        StartCoroutine(RoutineInvincible());
    }

    public void OnHeal()
    {
        f_lifeAmount++;

        for (int i = 0; i < list_Hearts.Count; i++)
        {
            if (i < f_lifeAmount) list_Hearts[i].gameObject.SetActive(true);
            else list_Hearts[i].gameObject.SetActive(false);
        }
    }

    public void SetBoostAmount(int amount)
    {
        i_boostAmount = amount;
    }

    private IEnumerator RoutineDeath()
    {
        yield return new WaitForSecondsRealtime(5f);
        SceneManager.LoadScene(0);
    }
    private IEnumerator RoutineInvincible()
    {
        yield return new WaitForSecondsRealtime(1f);
        b_Invincible = false;
    }
}
