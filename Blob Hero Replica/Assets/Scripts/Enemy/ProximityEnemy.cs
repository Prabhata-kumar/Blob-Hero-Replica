using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityEnemy : BaseEnemy
{
    public override void AttackPlayer(Player player)
    {
        // Reduce player's health when close enough
        if (Vector3.Distance(transform.position, player.transform.position) < detectionRange)
        {
            player.TakeDamage(damage);
        }
    }
}

