using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetatchFromParent : SingletonMonobehaviour<DetatchFromParent>
{
    private void OnEnable()
    {
        transform.parent = null;
    }
}
