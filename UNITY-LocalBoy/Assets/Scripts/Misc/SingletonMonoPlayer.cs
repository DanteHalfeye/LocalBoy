using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoPlayer : SingletonMonobehaviour<SingletonMonoPlayer>
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
