using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float fuerza;
    [SerializeField] LayerMask targetLayer;

    [SerializeField] UIManager uiManager;

    private void Awake()
    {
        if (this.gameObject.tag == "Player")
        {
            uiManager = FindAnyObjectByType<UIManager>();
            uiManager.SetShoot(this);
        }
    }
    // Te recomiendo que le pases el componente del rigidbody al metodo para que no lo llame cada disparo
    public void OnShoot(Vector2 direccion, GameObject bala) 
    {
        if (bala != null)
        {
            bala.transform.position = gameObject.transform.position;
            bala.GetComponent<Rigidbody2D>().velocity = direccion * fuerza;
        }
    }

    public Vector2 AutoShootDirection()
    {
        GameObject jugador;

        if (GameObject.Find("Player")) //usa un find with tag, gasta menos recursos (Ya le puse el tag)
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
}
