using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sc_Entity : MonoBehaviour
{
    [SerializeField]
    public float f_lifeAmount = 1f;
    public abstract void OnDamage();
}
