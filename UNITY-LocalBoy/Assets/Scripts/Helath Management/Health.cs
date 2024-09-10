using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public HealthStatsSO health;


    public void Heal(int amount)
    {
        health.Heal(amount);
    }
    public void TakeDamage(int amount)
    {
        health.TakeDamage(amount);
        if(health.currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Murió");
        gameObject.SetActive(false);
        
    }
    
}
