using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : MonoBehaviour
{
    public static PlayerActor instance;
    [SerializeField] HealthStatsSO health;


    //ARREGLAR BALAS
    private int currentAmmo;


    private InputManager inputManager;
    private Movement movement;

    bool isHolding, isShooting;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance);
        }
    }

    private void Start()
    {
        inputManager = GetComponent<InputManager>();
        movement = GetComponent <Movement>();
    }

    public bool OnHoldPress()
    {
        isHolding = !isHolding;
        return isHolding;
    }


    public float HpPercent
    {
        get { return health.MaxHealth / health.CurrentHealth; }
    }

    public float GetHp
    {
        get { return health.CurrentHealth; }
    }

    public void SetHp(int value)
    {
        health.CurrentHealth = value;
    }

    public void ModifyCurrentHp(int value)
    {
        health.CurrentHealth += value;
    }

    public void SetMaxHp(int value)
    {
        health.MaxHealth = value;
    }

    public void ModifyMaxHp(int value)
    {
        health.MaxHealth += value;
    }

    public int GetAmmo
    {
        get { return currentAmmo; }
    }

    public void AddAmmo(int value)
    {
        currentAmmo += value;
    }

    public void SetAmmo(int value)
    {
        currentAmmo = value;
    }

    public float GetMovementSpeed
    {
        get { return movement.Speed; }
    }

    public void SetMovementSpeed(float value)
    {
        movement.Speed = value;
    }

    public void ModifyMovementSpeed(float value)
    {
        movement.Speed += value;
    }

    public float GetAttackSpeed
    {
        get { return inputManager.AutoShootCD; }
    }

    public void SetAttackSPeed(float value)
    {
        inputManager.AutoShootCD = value;
    }

    public void ModifyAttackSpeed(float value)
    {
        inputManager.AutoShootCD += value;
    }

    public void PickUpItem(ItemSO item)
    {
        ItemManager.RegisterItem(item, this);

        Debug.Log("item " + item.name);

    }

    public void OnKill()
    {
        ItemEvents.TriggerEnemyKilled();
    }

    public void OnRoomEntered()
    {
        ItemEvents.TriggerOnRoomEntered();
    }

    private void OnStatChange()
    {
        ItemEvents.TriggerOnStatChange();
    }
}
