using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singularity : MonoBehaviour
{

    private void Awake()
    {
        StartCoroutine(Push());    
    }


    private IEnumerator Push()
    {
        float timer = 10f;
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 15f);
        Vector2 explosionCenter = transform.position;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("EnemyBullet"))
            {
                Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    // Calcula la dirección de la explosión y la distancia
                    Vector2 direction = (rb.position - explosionCenter).normalized;
                    float distance = Vector2.Distance(rb.position, explosionCenter);

                    // Calcula la magnitud de la fuerza según la distancia (inversamente proporcional)
                    float forceMagnitude = 10f * (1 - (distance / 10f));

                    // Aplica la fuerza de la explosión
                    rb.AddForce(direction * forceMagnitude, ForceMode2D.Impulse);
                }
            }
        }

        StartCoroutine(Push());


    }
}
