using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public HealthStatsSO health;

    public void SetHealth(int amount)
    {
        health.CurrentHealth = amount;

        if (health.CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        health.Heal(amount);
    }
    public void TakeDamage(int amount)
    {
        health.TakeDamage(amount);
        if(health.CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
        if (GetComponent<DestroyedEvent>() == null)
        {
            this.gameObject.SetActive(false);
        }
        DestroyedEvent destroyed = GetComponent<DestroyedEvent>();
        destroyed.CallDestroyedEvent();

    }
    
}
