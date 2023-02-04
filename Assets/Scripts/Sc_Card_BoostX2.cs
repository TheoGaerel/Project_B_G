using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Card_BoostX2 : Sc_Card
{
    public override void OnUse()
    {
        MoveToEndOfStack();
        Sc_Player.Instance.SetBoostAmount(2);
        Sc_PlayerController.Instance.StartDelayNextCard(reload);
    }
}
