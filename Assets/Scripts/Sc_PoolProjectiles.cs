using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_PoolProjectiles : MonoBehaviour
{
    private static Sc_PoolProjectiles sc_instance;

    public static Sc_PoolProjectiles Instance
    {
        get
        {
            if (sc_instance == null)
            {
                sc_instance = FindObjectOfType<Sc_PoolProjectiles>();
            }
            return sc_instance;
        }
    }

    [SerializeField]
    private List<Sc_Projectile> list_Projectiles = new List<Sc_Projectile>();
    [SerializeField]
    private GameObject projectilePrefab;

    public Sc_Projectile RequestProjectile()
    {

        foreach (Sc_Projectile proj in list_Projectiles)
        {
            if (proj.owner == null)
            {
                return proj;
            }
        }

        Sc_Projectile spawnedproj = Instantiate(projectilePrefab).GetComponent<Sc_Projectile>();
        spawnedproj.transform.SetParent(this.transform);
        list_Projectiles.Add(spawnedproj);
        return spawnedproj;

    }
}
