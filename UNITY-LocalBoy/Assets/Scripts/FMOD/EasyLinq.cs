using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EasyLinq : MonoBehaviour
{
    public static EasyLinq instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Mas de un linq script en la escena");
        }
        instance = this;
    }



}
