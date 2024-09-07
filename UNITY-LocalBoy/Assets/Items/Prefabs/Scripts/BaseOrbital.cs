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

    private void Awake()
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
    }
}

public enum rotationAxis
{
    forward,
    up,
    right
}

