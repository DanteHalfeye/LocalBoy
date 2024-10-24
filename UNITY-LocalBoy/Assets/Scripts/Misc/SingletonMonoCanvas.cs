using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoCanvas : SingletonMonobehaviour<SingletonMonoCanvas>
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
