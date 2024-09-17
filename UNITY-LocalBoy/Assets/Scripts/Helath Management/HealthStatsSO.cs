using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "newHealthStats", menuName = "Scriptable Objects/Health/HealthStats")]
public class HealthStatsSO : ScriptableObject
{
    [SerializeField]
    private int maxHealth, currentHealth;


    public void OnEnable()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = math.clamp(currentHealth, 0, maxHealth);
    }

    public int MaxHealth { get { return maxHealth; }  set { maxHealth = value; } }
    public int CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }
    
}
