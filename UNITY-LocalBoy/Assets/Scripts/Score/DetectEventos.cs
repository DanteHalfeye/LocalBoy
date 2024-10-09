using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEventos : MonoBehaviour
{
    public Counter counter;

    
    public void MuerteEnem()
    {
        counter.AddScore(20, false); //sumamos 20 puntos por enemigo muerto

        
    }
}
