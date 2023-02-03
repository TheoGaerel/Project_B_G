using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Player : Sc_Entity
{

    public override void OnDamage()
    {
        Debug.Log("Player is damaged");
        f_lifeAmount--;
        if (f_lifeAmount <= 0)
        {
            Debug.Log("Player is dead");
        }
    }
}
