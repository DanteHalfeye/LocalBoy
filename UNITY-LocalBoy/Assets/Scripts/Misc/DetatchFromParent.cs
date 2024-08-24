using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetatchFromParent : MonoBehaviour
{
    private void Awake()
    {
        transform.parent = null;
    }
}
