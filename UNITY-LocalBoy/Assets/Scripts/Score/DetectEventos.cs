using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DetectEventos : MonoBehaviour
{
    Counter counter;
    public void MuerteEnem()
    {
        counter.AddScore(20, false);
        //Debug.Log("muerte enem" + );
    }
}
