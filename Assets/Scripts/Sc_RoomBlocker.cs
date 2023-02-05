using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_RoomBlocker : MonoBehaviour
{
    public virtual void OnRoomEnter()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void OnRoomBattleFinish()
    {
        this.gameObject.SetActive(false);
    }
}
