using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseOrbital : MonoBehaviour
{
    [SerializeField]
    protected float speed;

    protected Transform padre;

    protected Vector3 rotationVector;

    [SerializeField]
    protected rotationAxis axis;
    [SerializeField]
    protected bool rotateAroundItself;

    protected virtual void Awake()
    {
        switch(axis)
        {
            case rotationAxis.forward:
                rotationVector = Vector3.forward; break;
            case rotationAxis.right:
                rotationVector = Vector3.right; break;
            case rotationAxis.up:
                rotationVector = Vector3.up; break;
        }

        padre = transform.parent;
        Debug.Log(padre.name);
    }
}

public enum rotationAxis
{
    forward,
    up,
    right
}

