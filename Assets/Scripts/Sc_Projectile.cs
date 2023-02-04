using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Projectile : MonoBehaviour
{
    public enum Team
    {
        Player,
        Enemies
    }

    public enum ProjectileType
    {
        Revolver = 0,
        Sniper = 1,
        Shotgun = 2,
        Enemy = 3
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

    [SerializeField]
    protected float f_Speed = 12f;
    [SerializeField]
    private List<GameObject> list_projectilesMesh = new List<GameObject>();

    public Team projectileTeam;
    public GameObject owner;

    private Vector3 startPosition;
    private Range range;
    private float f_speedMult = 1f;

    public void Init(Team team, GameObject owner, Range range, float speedMult, ProjectileType projectileType)
    {
        this.projectileTeam = team;
        this.owner = owner;

        this.range = range;
        this.transform.position = owner.transform.position;
        startPosition = owner.transform.position;
        this.f_speedMult = speedMult;


        foreach (GameObject go in list_projectilesMesh) if (go != null) go.SetActive(false);

        if (list_projectilesMesh[(int)projectileType] != null) list_projectilesMesh[(int)projectileType].SetActive(true);
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
