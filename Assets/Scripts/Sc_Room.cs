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

    [SerializeField]
    private Sc_Card rewardChoice1;
    [SerializeField]
    private Sc_Card rewardChoice2;
    [SerializeField]
    private Sc_Altar altarRoom;

    bool b_playerHere = false;
    int i_currentWave = -1;
    public void OnRoomEnter()
    {
        foreach (GameObject go in list_blockers) go.SetActive(true);
        b_playerHere = true;
        if (altarRoom) altarRoom.gameObject.SetActive(false);
        NextPhase();
    }

    private void Update()
    {
        if (!b_playerHere || i_currentWave == -1) return;

        if (i_currentWave < waves.Count)
        {
            Wave currentWave = waves[i_currentWave];
            foreach (Sc_Enemy enemy in currentWave.list_enemies)
            {
                if (enemy == null) continue;
                if (enemy.gameObject.activeSelf) return;
            }

            NextPhase();
        }
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
            if (enemy == null) continue;
            enemy.gameObject.SetActive(true);
        }
    }

    public void OnRoomFinish()
    {
        foreach (GameObject go in list_blockers) go.SetActive(false);
        StartCoroutine(RoutineDelayRewards());
    }

    private IEnumerator RoutineDelayRewards()
    {
        yield return new WaitForSeconds(0.25f);
        Sc_PlayerController.Instance.SetCanInteract(false);
        if (altarRoom) altarRoom.gameObject.SetActive(true);
        Sc_PanelChoice.Instance.ShowChoices(rewardChoice1, rewardChoice2);
    }
}

