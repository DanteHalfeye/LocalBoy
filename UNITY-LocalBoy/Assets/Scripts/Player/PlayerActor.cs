using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : SingletonMonobehaviour<PlayerActor>
{
    private float maxHp, currentHp,
        movementSpeed, attackSpeed;
    private int currentAmmo;





    bool isHolding, isShooting;

    [SerializeField] HealthSO Health;

    Movement movement;

    Shoot shoot;


    private void Start()
    {
        movement = GetComponent<Movement>();
        shoot = GetComponent<Shoot>();

    }

    public bool OnHoldPress()
    {
        isHolding = !isHolding;
        return isHolding;
    }


    public float HpPercent
    {
        get { return maxHp / currentHp; }
    }

    public float GetHp
    {
        get { return currentHp; }
    }

    public void SetHp(float value)
    {
        currentHp = value;
    }

    public void ModifyCurrentHp(float value)
    {
        currentHp += value;
    }

    public void SetMaxHp(float value)
    {
        maxHp = value;
    }

    public void ModifyMaxHp(float value)
    {
        maxHp += value;
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
        get { return movementSpeed; }
    }

    public void SetMovementSpeed(float value)
    {
        movementSpeed = value;
        movement.Speed = value;
    }

    public void ModifyMovementSpeed(float value)
    {
        movementSpeed += value;
        movement.Speed += value;
    }

    public float GetAttackSpeed
    {
        get { return attackSpeed; }
    }

    public void SetAttackSPeed(float value)
    {
        attackSpeed = value;
    }

    public void ModifyAttackSpeed(float value)
    {
        attackSpeed += value;
    }

    public void PickUpItem(ItemSO item)
    {
        ItemManager.RegisterItem(item, this);

        Debug.Log("item " + item.name);

    }

    private void OnKill()
    {
        ItemEvents.TriggerEnemyKilled();
    }

    private void OnRoomEntered()
    {
        ItemEvents.TriggerOnRoomEntered();
    }

    private void OnStatChange()
    {
        ItemEvents.TriggerOnStatChange();
    }
}
