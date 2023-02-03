using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sc_Entity : MonoBehaviour
{
    [SerializeField]
    protected float f_lifeAmount = 1f;
    public abstract void OnDamage();
}
