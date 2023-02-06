using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sc_Entity : MonoBehaviour
{
    [SerializeField]
    public float f_lifeAmount { get; protected set; } = 1f;
    public abstract void OnDamage();
}
