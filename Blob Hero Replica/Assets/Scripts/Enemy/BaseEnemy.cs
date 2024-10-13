using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float health;
    public float detectionRange;
    public float damage;
    public Animator animator;

    public virtual void DetectPlayer(Transform player)
    {
        // Code to detect player within a certain range
    }

    public virtual void AttackPlayer(Player player)
    {
        // Basic attack logic
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        // Base death behavior
        Destroy(gameObject);
    }
}

