using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoEventSystem : SingletonMonobehaviour<SingletonMonoEventSystem>
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
