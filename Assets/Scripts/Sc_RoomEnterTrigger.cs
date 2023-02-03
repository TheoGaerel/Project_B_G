using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_RoomEnterTrigger : MonoBehaviour
{
    private Sc_Room linkedRoom;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            linkedRoom.OnRoomEnter();
            this.gameObject.SetActive(false);
        }
    }
}
