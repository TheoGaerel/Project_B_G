using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Room : MonoBehaviour
{
    [SerializeField] private List<List<Sc_Enemy>> waves = new List<List<Sc_Enemy>>();

    [SerializeField] private List<GameObject> list_blockers = new List<GameObject>();

    public void OnRoomEnter()
    {

    }
}
