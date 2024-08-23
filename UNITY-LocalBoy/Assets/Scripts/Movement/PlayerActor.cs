using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : SingletonMonobehaviour<PlayerActor> 
{
    private float movingSpeed, holdingSpeed, shootCooldown, health, ammunition;
    bool isHolding, isShooting;

    [SerializeField] HealthSO Health;
 
    Movement movement;

    Shoot shoot;
    private void Start()
    {
        movement = GetComponent<Movement>();
        shoot = GetComponent<Shoot>();

    }
    /// <summary>
    /// Since items stack, the code checks the current speed and then adds it - it also saves the moving speed in another variable to protect the original 
    /// </summary>
    public void AddMovingSpeed(float amount)
    {
        movingSpeed = movement.Speed + amount;
        movement.Speed = movingSpeed;

    }
    public void SetMovingSpeed(float amount)
    {
        movingSpeed =  amount;
        movement.Speed = amount;
    }

    public void AddAmmo()
    {

    }
    public void SetAmmo()
    {

    }

    public bool OnHoldPress()
    {
        isHolding = !isHolding;
        return isHolding;
    }


}
