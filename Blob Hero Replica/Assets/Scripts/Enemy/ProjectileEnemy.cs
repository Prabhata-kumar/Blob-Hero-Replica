using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : BaseEnemy
{
    public GameObject projectilePrefab;

    public override void AttackPlayer(Player player)
    {
        // Shoot projectile towards player
        if (Vector3.Distance(transform.position, player.transform.position) < detectionRange)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            // Logic to make the projectile move towards player
        }
    }
}

