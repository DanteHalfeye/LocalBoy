using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRUEBA : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento del objeto

    private Rigidbody2D rb;

    void Start()
    {
        // Obtener el componente Rigidbody del GameObject
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Leer la entrada del teclado para movimiento horizontal y vertical
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Crear un vector de movimiento en 3D
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * moveSpeed;

        // Aplicar la velocidad al Rigidbody
        rb.velocity = movement;

    }



}
