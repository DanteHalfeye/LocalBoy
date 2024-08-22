using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //Lo voy a hacer medio a las patadas pero se va mejorando de a poquitos
    

    public float fuerza;
    public void OnShoot(Vector2 direccion, GameObject bala)
    {
        if (bala != null)
        {
            bala.transform.position = gameObject.transform.position;
            bala.GetComponent<Rigidbody2D>().velocity = direccion * fuerza;
        }
    }
}
