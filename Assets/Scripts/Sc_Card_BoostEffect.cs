using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Card_BoostEffect : Sc_Card
{
    [SerializeField]
    private int boostAmount = 2;
    public override void OnUse()
    {
        MoveToEndOfStack();
        Sc_Player.Instance.SetBoostAmount(Sc_Player.Instance.i_boostAmount + boostAmount);
        Sc_PlayerController.Instance.StartDelayNextCard(reload);
    }
}
