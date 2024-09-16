using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public float fuerza;
    [SerializeField] float autoShootRange;
    [SerializeField] LayerMask enemyLayer;

    int ammo;
    public int Ammo
    {
        get { return ammo; } set { ammo = value; }
    }


    public void OnShoot(Vector2 direccion, GameObject bala)
    {
        if (bala != null)
        {
            if(ammo > 0)
            {
                bala.transform.position = gameObject.transform.position;
                bala.GetComponent<Rigidbody2D>().velocity = direccion * fuerza;
                ammo--;
            }
            
        }
    }

    public Vector2 AutoShootDirection()
    {
        Collider2D enemyToShoot = Physics2D.OverlapCircle(transform.position, autoShootRange, enemyLayer);

        if (enemyToShoot != null)
        {
            GameObject enemigo = enemyToShoot.gameObject;
            
            Vector2 shootDirection = (enemigo.transform.position - gameObject.transform.position).normalized;

            return shootDirection;
        }
        else
        {
            return new Vector2(1,1);
        }
    }
}
