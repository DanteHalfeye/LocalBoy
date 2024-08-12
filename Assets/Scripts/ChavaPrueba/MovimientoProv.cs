using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoProv : MonoBehaviour
{
    public float velocidadMovimiento = 5f; // Velocidad de movimiento del personaje
    private Rigidbody2D rb;
    private Vector2 movimiento;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Obtener la entrada del usuario
        movimiento.x = Input.GetAxisRaw("Horizontal");
        movimiento.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // Mover el personaje
        rb.MovePosition(rb.position + movimiento * velocidadMovimiento * Time.fixedDeltaTime);
    }
}
