using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Projectile : MonoBehaviour
{
    private float f_Speed = 15f;
    public enum Team
    {
        Player,
        Enemies
    }

    public Team projectileTeam;
    public GameObject owner;
    public void Init(Team team, GameObject owner)
    {
        this.projectileTeam = team;
        this.owner = owner;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && projectileTeam == Team.Player)
        {
            Explode();
        }
        else if (other.CompareTag("Player") && projectileTeam == Team.Enemies)
        {
            other.GetComponent<Sc_Player>().OnDamage();
            Explode();
        }
        else if (other.CompareTag("Obstacle"))
        {
            Explode();
        }
    }

    private void Update()
    {
        this.transform.position += this.transform.forward * f_Speed * Time.deltaTime;
    }

    private void Explode()
    {
        this.gameObject.SetActive(false);
        owner = null;
    }
}
