using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float autoDisableTimer;
    float timer;
    bool recienDisparada = true;
    

    private void FixedUpdate()
    {
        if (gameObject.activeSelf && recienDisparada)
        {
            timer = autoDisableTimer;
            recienDisparada = false;
        }

        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if(timer <= 0)
        {
            gameObject.SetActive(false);
            recienDisparada=true;
        }
    }
}
