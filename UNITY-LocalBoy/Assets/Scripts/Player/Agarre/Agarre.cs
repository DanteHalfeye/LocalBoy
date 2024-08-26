using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agarre : MonoBehaviour
{
    GameObject enemigo;
    Rigidbody2D enemyRB;
    CircleCollider2D enemiesInRange;
    [SerializeField] float grabRange;
    [SerializeField] LayerMask enemyLayer;
    bool grabbing; //Esto es temporal se debe usar el state machine :D
    public void Agarrar() 
    {

        if (grabbing)
        {
            enemigo.transform.SetParent(null);
            Destroy(enemigo,1f); //Esto se cambiará despues a la forma en la que matemos enemigos, puede ser directamente pasando la vida del enmigo a 0
            grabbing = false;
        }

        Collider2D enemyToGrab = Physics2D.OverlapCircle(transform.position, grabRange, enemyLayer);

        if (enemyToGrab != null && !grabbing)
        {
            enemigo = enemyToGrab.gameObject;

            enemyRB = enemigo.GetComponent<Rigidbody2D>();
            enemyRB.simulated = false;

            

            enemigo.transform.SetParent(gameObject.transform, false);
            enemigo.transform.position =  new Vector3(0.5f, 0.5f, 0f) + gameObject.transform.localPosition;

            grabbing = true; //Esto se cambiaría en la maquina de estados
        }
        
        

    }

}
