using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Wave
{
    [SerializeField]
    public List<Sc_Enemy> list_enemies = new List<Sc_Enemy>();
}



public class Sc_Room : MonoBehaviour
{
    [SerializeField] private List<Wave> waves = new List<Wave>();

    [SerializeField] private List<GameObject> list_blockers = new List<GameObject>();
    bool b_playerHere = false;
    int i_currentWave = -1;
    public void OnRoomEnter()
    {
        Debug.Log("OnRoomEnter");
        foreach (GameObject go in list_blockers) go.SetActive(true);
        b_playerHere = true;
        NextPhase();
    }

    private void Update()
    {
        if (!b_playerHere || i_currentWave == -1) return;

        Wave currentWave = waves[i_currentWave];
        foreach (Sc_Enemy enemy in currentWave.list_enemies)
        {
            if (enemy.gameObject.activeSelf) return;
        }

        NextPhase();
    }

    private void NextPhase()
    {
        i_currentWave++;
        if (i_currentWave >= waves.Count)
        {
            OnRoomFinish();
            return;
        }

        Wave currentWave = waves[i_currentWave];
        foreach (Sc_Enemy enemy in currentWave.list_enemies)
        {
            enemy.gameObject.SetActive(true);
        }
    }

    public void OnRoomFinish()
    {
        Debug.Log("Room Finish");
    }
}

