using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidEnemy : BaseEnemy
{
    public GameObject liquidPrefab;

    protected override void Die()
    {
        // Spawn liquid at death location
        Instantiate(liquidPrefab, transform.position, Quaternion.identity);
        base.Die();
    }
}

