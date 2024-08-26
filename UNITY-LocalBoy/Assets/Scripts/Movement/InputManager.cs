using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Vector2 MoveInput, FireInput;
    public BulletPool bp;
    public Agarre agarre;

    private void Awake()
    {
        bp = GetComponent<BulletPool>();
        agarre = GetComponent<Agarre>();
    }

    public void OnAgarre()
    {
        Debug.Log("intentando agarrar");
        agarre.Agarrar();

    }

    public void OnMove(InputValue input)
    {
        MoveInput = input.Get<Vector2>();
    }

    public void OnFire(InputValue input)
    {
        FireInput = input.Get<Vector2>();
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
}
