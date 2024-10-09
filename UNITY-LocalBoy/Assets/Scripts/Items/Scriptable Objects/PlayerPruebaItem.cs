using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPruebaItem : MonoBehaviour
{
    public float velocidadMovimiento = 5f; // Velocidad de movimiento
    private Rigidbody2D rb;
    private Vector2 movimiento;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtener el componente Rigidbody2D
    }

    void Update()
    {
        // Obtener la entrada del jugador (flechas o WASD)
        float movimientoHorizontal = Input.GetAxisRaw("Horizontal");
        float movimientoVertical = Input.GetAxisRaw("Vertical");

        // Crear el vector de movimiento
        movimiento = new Vector2(movimientoHorizontal, movimientoVertical).normalized;

    }

    void FixedUpdate()
    {
        // Mover el personaje usando la física
        rb.MovePosition(rb.position + movimiento * velocidadMovimiento * Time.fixedDeltaTime);
    }
}
