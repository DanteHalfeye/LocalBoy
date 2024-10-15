using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float fuerza;
    public string tagObjetivo;
    GameObject jugador;
    [SerializeField] float desviacion;

    [SerializeField] UIManager uiManager;

    LayerMask selfLayer;

    public LineRenderer rayo; // El LineRenderer para el rayo
    [SerializeField] float duracionRayo = 0.1f; // Duración del rayo antes de desaparecer
    [SerializeField] float longitudRayo = 5f; // Longitud máxima del rayo

    public GameObject customShootPreview;

    public bool mostrandoRayo;

    private void Awake()
    {
        if (this.gameObject.tag == "Player")
        {
            uiManager = FindAnyObjectByType<UIManager>();
            uiManager.SetShoot(this);
        }

        if (GameObject.FindGameObjectWithTag("Player"))
        {
            jugador = GameObject.Find("Player");
        }
        else
        {
            jugador = null;
        }

        selfLayer = this.gameObject.layer;
        
    }

    private void FixedUpdate()
    {
        if (mostrandoRayo)
        {
            MostrarRayo(AutoShootDirection());
        }
        else
        {
            if (rayo != null) { rayo.enabled = false; }

            else if (customShootPreview != null) { customShootPreview.SetActive(false); }


        }
    }


    public void OnShoot(Vector2 direccion, GameObject bala) 
    {
        if (bala != null)
        {
            //AUDIO: Aquí se llamaría el audio del disparo de enemigo 
            bala.transform.position = gameObject.transform.position;
            bala.GetComponent<Rigidbody2D>().velocity = direccion * fuerza;
        }
    }
    public  void OnShoot(Vector2 direccion, GameObject bala, float force)
    {
        if (bala != null)
        {
            //AUDIO: Aquí se llamaría el audio del disparo de enemigo 
            bala.transform.position = gameObject.transform.position;
            bala.GetComponent<Rigidbody2D>().velocity = direccion * force;
        }
    }


    public void OnTripleShoot(Vector2 direccion, GameObject[] bala)
    {
        direccion.Normalize();
        Vector2 perpendicular = new Vector2(-direccion.y, direccion.x);

        // Disparo central
        OnShoot(direccion, bala[0]);

        // Disparo con desviación hacia un lado
        OnShoot(direccion + (perpendicular * desviacion), bala[1]);
        // Disparo con desviación hacia el otro lado
        OnShoot(direccion - (perpendicular * desviacion), bala[2]);
    }
    public void OnShotGunShot(Vector2 direccion, GameObject[] balasEscopeta)
    {
        direccion.Normalize();

        foreach (GameObject obj in balasEscopeta)
        {
            OnShoot(AutoShootDirection() + new Vector2(Random.Range(-0.2f,0.2f), Random.Range(-0.2f, 0.2f)), obj);
        }
    }


    public Vector2 AutoShootDirection()
    {
        

        

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
        if (rayo != null)
        {
            // Raycast para detectar colisiones
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direccion, longitudRayo, selfLayer);

            // Activar el LineRenderer
            rayo.enabled = true;

            // Establecer la posición de inicio del rayo
            rayo.SetPosition(0, transform.position); // Punto inicial en la posición del objeto que dispara

            // Si el Raycast detecta una colisión
            if (hit.collider != null)
            {
                // Establecer el punto final del rayo en el punto de colisión
                rayo.SetPosition(1, hit.point);
            }
            else
            {
                rayo.SetPosition(1, (Vector2)transform.position + (direccion * longitudRayo));
            }
        }
        else if(customShootPreview != null)
        {
            customShootPreview.SetActive(true);
            Vector2 direccionDelJugador = AutoShootDirection();// tu vector que apunta hacia el objetivo, normalizado
            float anguloEnGrados = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
            customShootPreview.transform.rotation = Quaternion.Euler(0, 0, anguloEnGrados+90);



        }
    }




}
