using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agarre : MonoBehaviour
{
    GameObject enemigo;
    public void Agarrar()
    {
        //Pruebas, no funcionar� as�
        enemigo = GameObject.Find("Enemigo");
        enemigo.transform.position = gameObject.transform.position + new Vector3(0.5f ,0.5f,0f);
        enemigo.transform.SetParent(gameObject.transform);

    }
}
