using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

public class InputManager : MonoBehaviour
{
    public Vector2 MoveInput, FireInput;
    BulletPool bp;
    Agarre agarre;

    float grabTimer = 0;
    float grabCD = 0.5f;

    float shootTimer = 0;
    float autoShootCD = 0.5f;

    private float pressTime = 0f;
    bool autoShootAllowed = true;

    bool shootAllowed;

    public float maxPressDuration = 0.2f; // Duración máxima para que se considere un disparo rápido
    public UIManager uiManager;
    private int bulletCount;

    private void Awake()
    {
        bp = GetComponent<BulletPool>();
        agarre = GetComponent<Agarre>();

        if (uiManager == null)
        {
            uiManager = FindObjectOfType<UIManager>();
        }
    }

    public void AutoShoot(InputAction.CallbackContext context)
    {
        Debug.Log("AUTOSHOOT");
        if (context.started)
        {
            pressTime = Time.time;
        }

        if (context.canceled)
        {
            if (Time.time - pressTime < 0.1f)
            {
                autoShootAllowed = true;
                Debug.Log("AutoDisparo");
                Shoot playerShoot = GetComponent<Shoot>();
                if (playerShoot != null)
                {
                    if (shootAllowed)
                    {
                        playerShoot.OnShoot(playerShoot.AutoShootDirection(), bp.RequerirBala());
                        shootAllowed = false;
                        shootTimer = autoShootCD;
                    }
                }
                autoShootAllowed = false;
            }
            else
            {
                autoShootAllowed = false;
            }
        }
    }

    public void OnAgarre()
    {
        if (grabTimer <= 0.1f)
        {
            int ammoGained = agarre.Agarrar(); // Obtener las balas del enemigo
            Debug.Log("Intentando agarrar");
            bulletCount += ammoGained; // Actualizamos la cantidad de balas
            uiManager.updateAmmo(bulletCount); // Actualizamos la UI
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
    public InputAction autoShootInput;

    private void OnEnable()
    {
        joystickInput.Enable();
        joystickInput.canceled += OnJoystickReleased;

        autoShootInput.Enable();
        autoShootInput.started += AutoShoot;
        autoShootInput.canceled += AutoShoot;

    }

    private void OnDisable()
    {
        joystickInput.canceled -= OnJoystickReleased;
        joystickInput.Disable();

        autoShootInput.started -= AutoShoot;
        autoShootInput.canceled -= AutoShoot;
        autoShootInput.Disable();
    }

    private void OnJoystickReleased(InputAction.CallbackContext context)
    {
        if (GetComponent<Shoot>() != null)
        {
            if (FireInput.magnitude > 0.75f && shootAllowed) // No se dispare solo cuando el stick pasa cerca al 0,0
            {
                GetComponent<Shoot>().OnShoot(FireInput.normalized, bp.RequerirBala());
                shootAllowed = false;
                shootTimer = autoShootCD;
            }
        }
    }

    private void Update()
    {
        if (grabTimer > 0)
        {
            grabTimer -= Time.deltaTime;
        }

        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }
        else
        {
            shootAllowed = true;
        }
    }

    public float AutoShootCD { get { return autoShootCD; } set { autoShootCD = value; } }
}
