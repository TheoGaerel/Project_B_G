using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Enemy : Sc_Entity
{
    public override void OnDamage()
    {
        Debug.Log("Enemy is damaged");
        f_lifeAmount--;
        if (f_lifeAmount <= 0)
        {
            Debug.Log("Player is dead");
        }
    }

    private void Update()
    {
        Behavior();
    }


    protected virtual void Behavior()
    {

    }
}
