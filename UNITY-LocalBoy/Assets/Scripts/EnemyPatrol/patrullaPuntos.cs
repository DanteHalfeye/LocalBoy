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

        // Si la distancia es menor a la distancia mínima, cambiamos al siguiente punto
        if (Vector2.Distance(transform.position, puntosMovimiento[siguientePaso]) < distanciaMinima)
        {
            siguientePaso += 1;
            if (siguientePaso >= puntosMovimiento.Length)
            {
                siguientePaso = 0; // Reiniciamos para que patrulle cíclicamente
            }
            Girar();
        }
    }

    private void Girar()
    {
        Vector2 direccion = puntosMovimiento[siguientePaso] - (Vector2)transform.position; // Dirección hacia el siguiente punto
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg; // Calcula el ángulo de la dirección
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angulo - 90)); // Ajustamos el ángulo restando 90°
    }
}

