using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sc_Player : Sc_Entity
{
    [SerializeField]
    private List<Image> list_Hearts = new List<Image>();

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
}
