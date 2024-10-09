using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DetectEventos : MonoBehaviour
{
   public Counter counter;

    private void Awake()
    {
       counter = GetComponent<Counter>();
    }
    public void MuerteEnem()
    {
        counter.AddScore(20, false); //sumamos 20 puntos por enemigo muerto
        
        //debg temporal
        Debug.Log("puntuacion actual" + counter.currentScore);
        //Debug.Log("muerte enem" + );
    }
}
