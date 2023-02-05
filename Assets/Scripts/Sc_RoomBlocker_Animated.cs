using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_RoomBlocker_Animated : Sc_RoomBlocker
{
    [SerializeField]
    private Animator animator;
    public override void OnRoomEnter()
    {
        animator.SetBool("b_Open", false);
    }

    public override void OnRoomBattleFinish()
    {
        animator.SetBool("b_Open", true);
    }
}
