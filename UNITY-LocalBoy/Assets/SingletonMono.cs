using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMono : SingletonMonobehaviour<SingletonMono>
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
