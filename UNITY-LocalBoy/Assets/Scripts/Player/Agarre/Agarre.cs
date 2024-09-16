using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Agarre : MonoBehaviour
{
    GameObject enemigo;
    Rigidbody2D enemyRB;
    [SerializeField] float grabRange;
    [SerializeField] LayerMask enemyLayer;
    bool grabbing;
    public bool Grabbing { get { return grabbing; } }
    bool lastGrabbingState;
    [SerializeField] float grabCD;
    float grabCDTimer;

    bool canGrab = true;

    private void Update()
    {
        if (grabCDTimer > 0)
        { 
            grabCDTimer -= Time.deltaTime; 
            canGrab = false;
        }
        else { canGrab = true; }

    }


    public void Agarrar() 
    {

        if(!canGrab)
        {
            Debug.Log("Grab en CD");
        }

        if (grabbing)
        {
            enemigo.transform.SetParent(null);
            enemigo.GetComponent<Health>().SetHealth(0);
            grabbing = false;
        }
        else
        {
            Collider2D enemyToGrab = Physics2D.OverlapCircle(transform.position, grabRange, enemyLayer);

            if (enemyToGrab != null && !grabbing && canGrab)
            {
                enemigo = enemyToGrab.gameObject;

                enemigo.GetComponent<FireWeapon>().enabled = false;

                enemyRB = enemigo.GetComponent<Rigidbody2D>();
                enemyRB.simulated = false;

                GetComponent<Shoot>().Ammo += 5;


                enemigo.transform.SetParent(gameObject.transform, false);
                enemigo.transform.position = transform.position + Vector3.one * 0.5f;

                grabbing = true; //Esto se cambiaría en la maquina de estados
            }
        }

        


        if (!grabbing && lastGrabbingState) //Si dejo de agarrar
        {
            grabCDTimer = grabCD;
        }

        lastGrabbingState = grabbing;



    }

}
