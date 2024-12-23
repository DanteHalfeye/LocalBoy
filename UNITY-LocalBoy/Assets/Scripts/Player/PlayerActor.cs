using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System;

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

    [SerializeField]
    private int currentHealth;
    [SerializeField]
    private int currency;

    [SerializeField]
    private TMPro.TextMeshProUGUI _textMeshPro;

    private bool _isUpdatingStats = false;

    private void OnEnable()
    {
        _textMeshPro.text = "MONEY: " + currency;
    }
    public int Currency
    {
        get { return currency; }
        set 
        { 
            currency = value;
            if (!_isUpdatingStats)
            {
                _isUpdatingStats = true;
                ItemEvents.TriggerOnStatChange();
                _isUpdatingStats = false;
            }
            _textMeshPro.text = "MONEY: " + currency;
        }
    }

    private void OnDestroy()
    {
        List<Guid> currentItems = ItemManager.GetActiveItemIdsForActor(this);

        foreach (var item in currentItems)
        {
            ItemManager.UnregisterItem(item, this);
            TriggerEffect.Unsubscribe(item);
        }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = math.clamp(value, 0, maxHealth);
            if (!_isUpdatingStats)
            {
                _isUpdatingStats = true;
                ItemEvents.TriggerOnStatChange();
                _isUpdatingStats = false;
            }
        }
    }


    public float HpPercent
    {
        get { return (float)currentHealth / maxHealth; }
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
        set
        {
            _dashSpeed = value;
            if (!_isUpdatingStats)
            {
                _isUpdatingStats = true;
                ItemEvents.TriggerOnStatChange();
                _isUpdatingStats = false;
            }
        }
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

        if (!_isUpdatingStats)
        {
            _isUpdatingStats = true;
            ItemEvents.TriggerOnStatChange();
            _isUpdatingStats = false;
        }
    }

    public void ModifyCurrentHp(int value)
    {
        currentHealth += value;

        currentHealth = math.clamp(currentHealth, 0, maxHealth);

        Debug.Log(currentHealth);

        if (!_isUpdatingStats)
        {
            _isUpdatingStats = true;
            ItemEvents.TriggerOnStatChange();
            _isUpdatingStats = false;
        }
    }

    public void SetMaxHp(int value)
    {
        maxHealth = value;
        if (!_isUpdatingStats)
        {
            _isUpdatingStats = true;
            ItemEvents.TriggerOnStatChange();
            _isUpdatingStats = false;
        }
    }

    public void ModifyMaxHp(int value)
    {
        maxHealth += value;
        if (!_isUpdatingStats)
        {
            _isUpdatingStats = true;
            ItemEvents.TriggerOnStatChange();
            _isUpdatingStats = false;
        }
    }

    public float GetMovementSpeed
    {
        get { return _maxSpeed; }
    }

    public void SetMovementSpeed(float value)
    {
        _maxSpeed = value;
        if (!_isUpdatingStats)
        {
            _isUpdatingStats = true;
            ItemEvents.TriggerOnStatChange();
            _isUpdatingStats = false;
        }
    }

    public void ModifyMovementSpeed(float value)
    {
        _maxSpeed += value;
        if (!_isUpdatingStats)
        {
            _isUpdatingStats = true;
            ItemEvents.TriggerOnStatChange();
            _isUpdatingStats = false;
        }
    }

    public float GetAttackSpeed
    {
        get { return _dashCooldown; }
    }

    public void SetAttackSpeed(float value)
    {
        _dashCooldown = value;
        if (!_isUpdatingStats)
        {
            _isUpdatingStats = true;
            ItemEvents.TriggerOnStatChange();
            _isUpdatingStats = false;
        }
    }

    public void ModifyAttackSpeed(float value)
    {
        _dashCooldown *= ((100f-value) * 0.01f);
        Debug.Log(value);
        if (!_isUpdatingStats)
        {
            _isUpdatingStats = true;
            ItemEvents.TriggerOnStatChange();
            _isUpdatingStats = false;
        }
    }

    public void PickUpItem(ItemSO item)
    {
        ItemManager.RegisterItem(item, this);

        Debug.Log("item " + item.name);

    }
}
