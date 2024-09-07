using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Periodico : BaseOrbital
{

    private void Update()
    {
        transform.RotateAround(padre.position, rotationVector, speed*Time.deltaTime);
        if (!rotateAroundItself)
        {
            transform.rotation = Quaternion.identity;
        }
    }

}
