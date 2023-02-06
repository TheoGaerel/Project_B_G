using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_RoomBlocker : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private AudioSource audioSource;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponentInChildren<AudioSource>();
    }
    public virtual void OnRoomEnter()
    {
        if (animator) animator.SetBool("b_Open", false);
        else this.gameObject.SetActive(true);
        if (audioSource) audioSource.Play();
    }

    public virtual void OnRoomBattleFinish()
    {
        if (animator) animator.SetBool("b_Open", true);
        else this.gameObject.SetActive(false);
        if (audioSource) audioSource.Play();
    }
}
