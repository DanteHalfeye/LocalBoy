using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalk : MonoBehaviour
{
    Vector3 _direcction;
    [SerializeField] float speed;
    
    private void Update()
    {
        int choice =Mathf.Clamp( Random.Range(0, 4), 0, 4);
        if (choice == 0)
        {
            _direcction = Vector3.right;
        }
        else if (choice == 1)
        {
            _direcction = Vector3.up;
        }
        else if (choice == 2)
        {
            _direcction = Vector3.left;
        }
        else
        {
            _direcction = Vector3.down;
        }
        transform.position += _direcction * speed;
    }
}
