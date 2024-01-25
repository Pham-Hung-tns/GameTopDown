using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatterns : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float damage;

    public Projectile GetProjectTile()
    {
        Projectile projectile = Instantiate(projectilePrefab);
        projectile.Direction = Vector3.right;
        projectile.transform.position = transform.position;
        projectile.Damage = damage;
        return projectile;
    }
}
