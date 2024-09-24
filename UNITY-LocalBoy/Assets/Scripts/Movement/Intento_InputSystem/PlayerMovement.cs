using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference moveActionToUse, buttonPress;
    [SerializeField] private float speedMovement;

    public void Button()
    {
        Debug.Log("Presionando botón");
    }

    void Update()
    {
        Vector2 moveDirection = moveActionToUse.action.ReadValue<Vector2>();
        transform.Translate(moveDirection * speedMovement * Time.deltaTime);

        if (buttonPress.action.WasPressedThisFrame())
        {
            Button();
        }
    }
}
