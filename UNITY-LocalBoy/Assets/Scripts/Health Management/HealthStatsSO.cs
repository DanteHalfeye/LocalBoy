using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(fileName = "newHealthStats", menuName = "Scriptable Objects/Health/HealthStats")]
public class HealthStatsSO : ScriptableObject
{
    [SerializeField]
    private int maxHealth, currentHealth;
    public UnityAction OnHealthChanged;


    public void OnEnable()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke();
    }

    public void TakeDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        currentHealth = math.clamp(currentHealth, 0, maxHealth);
        OnHealthChanged?.Invoke();
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = math.clamp(currentHealth, 0, maxHealth);
        OnHealthChanged?.Invoke();
    }

    public int MaxHealth { get { return maxHealth; }  set { maxHealth = value; } }
    public int CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }
    
}
