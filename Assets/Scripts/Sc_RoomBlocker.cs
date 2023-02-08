using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sc_RoomBlocker : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponentInChildren<AudioSource>();
        if (animator) animator.SetBool("b_Open", true);
        if (GetComponent<BoxCollider>()) GetComponent<BoxCollider>().enabled = false;
        if (GetComponent<NavMeshObstacle>()) GetComponent<NavMeshObstacle>().enabled = false;
    }
    public virtual void OnRoomEnter()
    {
        if (animator)
        {
            animator.SetBool("b_Open", false);
            if (GetComponent<BoxCollider>()) GetComponent<BoxCollider>().enabled = true;
            if (GetComponent<NavMeshObstacle>()) GetComponent<NavMeshObstacle>().enabled = true;
        }
        else this.gameObject.SetActive(true);
        if (audioSource) audioSource.Play();
    }

    public virtual void OnRoomBattleFinish()
    {
        if (animator)
        {
            animator.SetBool("b_Open", true);
            if (GetComponent<BoxCollider>()) GetComponent<BoxCollider>().enabled = false;
            if (GetComponent<NavMeshObstacle>()) GetComponent<NavMeshObstacle>().enabled = false;
        }
        else this.gameObject.SetActive(false);
        if (audioSource) audioSource.Play();
    }
}
