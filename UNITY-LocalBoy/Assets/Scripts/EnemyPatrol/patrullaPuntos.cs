using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrullaPuntos : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private Vector2[] puntosMovimiento; // Cambiado a Vector2 para usar coordenadas
    [SerializeField] private float distanciaMinima;
    private int siguientePaso = 0;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Girar();
    }

    private void Update()
    {
        // Movemos al enemigo hacia la siguiente coordenada en puntosMovimiento
        transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[siguientePaso], velocidadMovimiento * Time.deltaTime);

        // Si la distancia es menor a la distancia m�nima, cambiamos al siguiente punto
        if (Vector2.Distance(transform.position, puntosMovimiento[siguientePaso]) < distanciaMinima)
        {
            siguientePaso += 1;
            if (siguientePaso >= puntosMovimiento.Length)
            {
                siguientePaso = 0; // Reiniciamos para que patrulle c�clicamente
            }
            Girar();
        }
    }

    private void Girar()
    {
        // Giramos al enemigo en funci�n de la direcci�n en que se mueve
        if (transform.position.x < puntosMovimiento[siguientePaso].x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
