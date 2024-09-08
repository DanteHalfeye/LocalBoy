using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calaca : BaseOrbital
{
    void Update()
    {
        transform.RotateAround(padre.position, rotationVector, speed * Time.deltaTime);
        if (!rotateAroundItself)
        {
            transform.rotation = Quaternion.identity;
        }
        Vector3 position = transform.position;
        position.z = 0f; // Forzamos el eje Z a 0
        transform.position = position;
    }
}
