using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Vector2 MoveInput, FireInput;
     BulletPool bp;
     Agarre agarre;

    float grabTimer = 0;
    float grabCD = 0.5f;

    float shootTimer = 0;
    float autoShootCD = 0.5f;


    private void Awake()
    {
        bp = GetComponent<BulletPool>();
        agarre = GetComponent<Agarre>();
    }

    public void AutoShoot()
    {
        if (shootTimer <= 0.1f)
        {
            Debug.Log("AutoDisparo");
            Shoot playerShoot = GetComponent<Shoot>();
            if (playerShoot != null)
            {
                if (FireInput.magnitude < 0.75f)
                {
                    playerShoot.OnShoot(playerShoot.AutoShootDirection(), bp.RequerirBala());
                }
            }
            shootTimer = autoShootCD;
        }

    }

    public void OnAgarre()
    {
        if (grabTimer <= 0.1f)
        {
            agarre.Agarrar();
            Debug.Log("Intentando agarrar");
            grabTimer = grabCD;
        }

    }

    public void OnMove(InputAction.CallbackContext callback)
    {
        MoveInput = callback.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext callback)
    {
        FireInput = callback.ReadValue<Vector2>();
    }

    public InputAction joystickInput;

    private void OnEnable()
    {
        joystickInput.Enable();
        joystickInput.canceled += OnJoystickReleased;
        
    }

    private void OnDisable()
    {
        joystickInput.canceled -= OnJoystickReleased;
        joystickInput.Disable();
    }

    private void OnJoystickReleased(InputAction.CallbackContext context)
    {
        if(GetComponent<Shoot>() != null)
        {
            if(FireInput.magnitude > 0.75f) //No se dispare solo cuando el stick pasa cerca al 0,0
            {
                GetComponent<Shoot>().OnShoot(FireInput.normalized, bp.RequerirBala());
            }
        }
    }


    private void Update()
    {
        if(grabTimer > 0)
        {
            grabTimer -= Time.deltaTime;
        } 

        if(shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }
    }
}
