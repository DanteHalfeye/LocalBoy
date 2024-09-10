using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "newHealthStats", menuName = "Scriptable Objects/Health/HealthStats")]
public class HealthStatsSO : ScriptableObject
{
    public int maxHealth, currentHealth;


    public void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = math.clamp(currentHealth, 0, maxHealth);
    }

    
}
