using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        Debug.Log("Player hit by : " + collision.collider.gameObject.name);
        OnDamage();
    }

    public override void OnDamage()
    {
        f_lifeAmount--;

        for (int i = 0; i < list_Hearts.Count; i++)
        {
            if (i < f_lifeAmount) list_Hearts[i].gameObject.SetActive(true);
            else list_Hearts[i].gameObject.SetActive(false);
        }

        if (f_lifeAmount <= 0)
        {
            Debug.Log("Player is dead");
        }
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
}
