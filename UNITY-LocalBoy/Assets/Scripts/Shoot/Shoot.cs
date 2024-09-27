using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float fuerza;
    public string tagObjetivo;
    [SerializeField] float desviacion;

    [SerializeField] UIManager uiManager;

    public LineRenderer rayo; // El LineRenderer para el rayo
    public float duracionRayo = 0.1f; // Duraci�n del rayo antes de desaparecer
    public float longitudRayo = 5f; // Longitud m�xima del rayo

    public bool mostrandoRayo;

    private void Awake()
    {
        if (this.gameObject.tag == "Player")
        {
            uiManager = FindAnyObjectByType<UIManager>();
            uiManager.SetShoot(this);
        }
    }

    private void FixedUpdate()
    {
        if (mostrandoRayo)
        {
            MostrarRayo(AutoShootDirection());
        }
        else
        {
            rayo.enabled = false;
        }
    }


    public void OnShoot(Vector2 direccion, GameObject bala) 
    {
        if (bala != null)
        { 
            bala.transform.position = gameObject.transform.position;
            bala.GetComponent<Rigidbody2D>().velocity = direccion * fuerza;
        }
    }


    public void OnTripleShoot(Vector2 direccion, GameObject[] bala)
    {
        direccion.Normalize();
        Vector2 perpendicular = new Vector2(-direccion.y, direccion.x);

        // Disparo central
        OnShoot(direccion, bala[0]);

        // Disparo con desviaci�n hacia un lado
        OnShoot(direccion + (perpendicular * desviacion), bala[1]);
        // Disparo con desviaci�n hacia el otro lado
        OnShoot(direccion - (perpendicular * desviacion), bala[2]);
    }

    public Vector2 AutoShootDirection()
    {
        GameObject jugador;

        if (GameObject.FindGameObjectWithTag("Player")) 
        {
             jugador = GameObject.Find("Player");
        }
        else
        {
            jugador = null;
        }

        if (jugador != null)
        {
            
            Vector2 shootDirection = (jugador.transform.position - gameObject.transform.position).normalized;
            return shootDirection;
        }
        else
        {
            return new Vector2(1, 1); // No necesitas un else, ya que si no hay jugador no deberian spawnear enemigos
        }
    }



    private void MostrarRayo(Vector2 direccion)
    {
        // Raycast para detectar colisiones
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direccion, longitudRayo);

        // Activar el LineRenderer
        rayo.enabled = true;

        // Establecer la posici�n de inicio del rayo
        rayo.SetPosition(0, transform.position); // Punto inicial en la posici�n del objeto que dispara

        // Si el Raycast detecta una colisi�n
        if (hit.collider != null)
        {
            // Verifica si el objeto tiene el tag deseado
            if (hit.collider.CompareTag(tagObjetivo))
            {
                Debug.Log("Colisi�n con el objeto: " + hit.collider.gameObject.name);
            }

            // Establecer el punto final del rayo en el punto de colisi�n
            rayo.SetPosition(1, hit.point);
        }
        else
        {
            rayo.SetPosition(1, (Vector2)transform.position + (direccion * longitudRayo));
        }
    }




}
