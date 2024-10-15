using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class PlayerActor : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;
    [SerializeField] private float _acceleration = 0.5f;
    [SerializeField] private float _deceleration = 0.5f;
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _dashSpeed = 20f;
    [SerializeField] private float _dashDuration = 0.2f;  
    [SerializeField] private float _dashCooldown = 1f; 

    private int currentHealth;
    private int currency;

    public int Currency
    {
        get { return currency; }
        set { currency = value; }
    }

    public int MaxHp
    {
        get { return maxHealth; }
    }

    public int CurrentHealth
    {
        get { return currentHealth;  }
        set { currentHealth = value; }
    }

    public float HpPercent
    {
        get { return maxHealth / currentHealth; }
    }

    public float Acceleration
    {
        get { return _acceleration; }
    }

    public float Deceleration
    {
        get { return _deceleration; }
    }

    public float MaxSpeed
    {
        get { return _maxSpeed; }
    }

    public float DashSpeed
    {
        get { return _dashSpeed; }
    }

    public float DashDuration
    {
        get { return _dashDuration; }
    }

    public float DashCooldown
    {
        get { return _dashCooldown; }
    }

    public void SetHp(int value)
    {
        currentHealth = value;

        currentHealth = math.clamp(currentHealth, 0, maxHealth);
    }

    public void ModifyCurrentHp(int value)
    {
        currentHealth += value;

        currentHealth = math.clamp(currentHealth, 0, maxHealth);
    }

    public void SetMaxHp(int value)
    {
        maxHealth = value;
    }

    public void ModifyMaxHp(int value)
    {
        maxHealth += value;
    }

    public float GetMovementSpeed
    {
        get { return _maxSpeed; }
    }

    public void SetMovementSpeed(float value)
    {
        _maxSpeed = value;
    }

    public void ModifyMovementSpeed(float value)
    {
        _maxSpeed += value;
    }

    public float GetAttackSpeed
    {
        get { return _dashCooldown; }
    }

    public void PickUpItem(ItemSO item)
    {
        ItemManager.RegisterItem(item, this);

        Debug.Log("item " + item.name);

    }
}
