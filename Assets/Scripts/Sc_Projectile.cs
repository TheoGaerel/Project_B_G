using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Projectile : MonoBehaviour
{
    [SerializeField]
    protected float f_Speed = 12f;
    public enum Team
    {
        Player,
        Enemies
    }

    public enum Range
    {
        Short_8 = 8,
        Medium_15 = 15,
        Far_100 = 100
    }

    public enum Reload
    {
        Fast_0_5,
        Medium_1,
        Slow_2
    }

    public enum Recoil
    {
        None_0 = 0,
        Medium_1 = 1,
        Heavy_2 = 2
    }

    public Team projectileTeam;
    public GameObject owner;

    private Vector3 startPosition;
    private Range range;
    private float f_speedMult = 1f;

    public void Init(Team team, GameObject owner, Range range, float speedMult)
    {
        this.projectileTeam = team;
        this.owner = owner;

        this.range = range;
        this.transform.position = owner.transform.position;
        startPosition = owner.transform.position;
        this.f_speedMult = speedMult;
        if (team == Team.Player)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
            this.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && projectileTeam == Team.Player)
        {
            Disable();
            other.GetComponent<Sc_Enemy>().OnDamage();
        }
        else if (other.CompareTag("Player") && projectileTeam == Team.Enemies)
        {
            other.GetComponent<Sc_Player>().OnDamage();
            Disable();
        }
        else if (other.CompareTag("Obstacle"))
        {
            Disable();
        }
    }

    private void Update()
    {
        this.transform.position += this.transform.forward * f_Speed * Time.deltaTime * f_speedMult;
        if (Vector3.Distance(this.transform.position, startPosition) > (int)range)
        {
            Disable();
        }
    }

    private void Disable()
    {
        this.gameObject.SetActive(false);
        owner = null;
    }
}
